using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private AudioSource Theaudio;
    public AudioClip zombiesound;
    public Transform player;
    public Transform[] waypoints;
    private UI ui;
    public Gamemanager gamemanager;
    private NavMeshAgent nvAgent;
    private Animator _animator;
    private CapsuleCollider cc;
    private Boss boss;
    [SerializeField]
    private Slider m_hpbar;

    public int max_hp = 50;

    public float patrolSpeed = 3f; // 순찰 이동속도

    public bool isDead = false;
    public bool isTrace = false;
    public bool isAttack = false;
    public bool isHit = false;
    public bool isWalk = false;

    public bool isatk = false;

    public float stoptime=0.25f;

    public float dist;
    public float Attackdist=1f;

    public int currentWaypointIndex = 0;
    public int m_HP = 50;
    public int m_Atk = 10;

    public Vector3 dir;

    public float attackInterval = 1f; // 공격 주기 (초)

    public void Awake()
    {
        nvAgent = GetComponent<NavMeshAgent>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Theaudio = GetComponent<AudioSource>();
        Theaudio.clip = zombiesound;
        _animator = this.gameObject.GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider>();
    }


    // Update is called once per frame
    void Update()
    {
        //if(!isDead) { Theaudio.Play(); }

        if (isDead)
        {
            return;
        }
        Patrol();
        dist = Vector3.Distance(transform.position, player.position);

        Trace();
        HandleHP();
    }
    
    void Patrol()
    {
        if (isTrace == false && isAttack == false && isHit == false)
        {
            
            Transform currentWaypoint = waypoints[currentWaypointIndex];
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, patrolSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.1f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

                // waypoint에 도달하면 반대 방향을 보도록 변경
                Vector3 nextWaypointDirection = (waypoints[currentWaypointIndex].position - transform.position).normalized;
                transform.LookAt(transform.position + nextWaypointDirection);
            }           
        }
    }


    void Trace()
    {
        if (isTrace == true && stoptime >= 0.25f && isHit == false)
        {
            nvAgent.acceleration = 8;
            nvAgent.speed = 6.5f;
            _animator.SetBool("isTrace", true);
            dir = player.transform.position;
            nvAgent.SetDestination(dir);

            if (dist <= Attackdist)
            {
                isTrace = false;
                isAttack = true;
                StartCoroutine(StartAttackCo());
                _animator.SetBool("isTrace", false);
            }
            else
                _animator.SetBool("isTrace", true);
        }
    }

    IEnumerator StartAttackCo()
    {
        nvAgent.velocity = Vector3.zero;
        nvAgent.acceleration = 0;
        if (!isatk)
        {
            _animator.SetBool("isAttack", true);
            player.gameObject.GetComponent<Player>().TakeDamage(m_Atk);
            isatk = true;
        }
        yield return new WaitForSeconds(attackInterval);
        isatk = false;
        _animator.SetBool("isAttack", false);
        isAttack = false;
        isTrace = true;
    }

    void OnHit(int damage)
    {
        if(!isHit)
        {
            isHit = true;
            StartCoroutine(OnHitCo(damage));
        }
    }

    IEnumerator OnHitCo(int damage)
    {
        m_HP -= damage;
        if (gamemanager.isSkill == false)
        {
            player.GetComponent<Player>().p_power += 5f;
        }
        nvAgent.speed = 0;

        if (m_HP <= 0)
        {
            isDead = true;
            m_hpbar.value = 0;
            Theaudio.Stop();
            _animator.SetTrigger("isDead");

            cc.enabled = false;
            nvAgent.velocity = Vector3.zero;
            Destroy(gameObject, 1.5f);
            
        }
        yield return new WaitForSeconds(stoptime);

        nvAgent.speed = patrolSpeed;
        isHit = false;
    }

    void HandleHP()
    {

        m_hpbar.value = (float)m_HP / (float)max_hp;

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            isTrace = true;
            isAttack = true;
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.damage);

        }
    }
}