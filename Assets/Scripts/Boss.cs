using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



public class Boss : MonoBehaviour
{
    private AudioSource Theaudio;
    public AudioClip zombiesound;

    [SerializeField]
    private Slider boss_hpbar;
    public List<string> Soundlist = new List<string>();
    private Player player;
    public Transform playerTr;
    public Transform[] waypoints;

    private NavMeshAgent navagent;
    private Animator _animator;
    private CapsuleCollider cc;
    public Gamemanager gamemanager;
    private float patrolSpeed = 1.5f; // ���� �ӵ�

    public float viewRange = 15f; // �þ� ����
    public float viewAngle = 120f; // �þ� ��
    public float distance;
    public float attackdistance = 1f;

    bool isWalk = false;
    public bool isTrace = false;
    bool isAttack = false;
    bool isDead = false;
    bool isBossHit = false;

    int currentWaypointIndex = 0;
    int max_hp = 150;
    int b_Hp = 150;
    int b_Atk = 20;

    public Vector3 dir;

    private void Awake()
    {
        navagent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        cc=GetComponent<CapsuleCollider>();
        Theaudio=GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    { 
        if(this.gameObject!=null)
        {
            StartCoroutine(SoundCo());
        }

        if (CanSeePlayer() && !isAttack)
        {
            Trace();
        }
        Patrol();
        distance = Vector3.Distance(transform.position, playerTr.position);
        HandleHP();
        if (b_Hp <= 0)
        {
            StopCoroutine(AttackCo());
            isDead = true;
            navagent.velocity = Vector3.zero;
            navagent.acceleration = 0f;
            _animator.SetTrigger("isDead");
            Theaudio.Stop();
            cc.enabled = false;
            Destroy(gameObject, 1.5f);
            
        }

    }

    void Patrol()
    {
        if (isTrace == false)
        {
            Transform currenWaypoint = waypoints[currentWaypointIndex];
            transform.position = Vector3.MoveTowards(transform.position, currenWaypoint.position, patrolSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, currenWaypoint.position) < 0.1f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

                // waypoint�� �����ϸ� �ݴ� ������ ������ ����
                Vector3 nextWaypointDirection = (waypoints[currentWaypointIndex].position - transform.position).normalized;
                transform.LookAt(transform.position + nextWaypointDirection);
            }
        }
    }

    void Trace()
    {
        if (isTrace == true)
        {
            navagent.speed = 2.5f;
            navagent.acceleration = 8f;
            _animator.SetBool("isTrace", true);
            _animator.SetBool("isWalk", false);
            _animator.SetBool("isAttack", false);
            dir = playerTr.transform.position;
            navagent.SetDestination(dir);
            Attack();
        }
        else
        {
            isTrace = false;
            _animator.SetBool("isWalk", true);
            _animator.SetBool("isTrace", false);
            navagent.speed = patrolSpeed;
        }
    }

    IEnumerator AttackCo()
    {
        navagent.velocity = Vector3.zero;
        navagent.acceleration = 0f;
        _animator.SetBool("isTrace", false);
        _animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(1.2f);
        player.TakeDamage(b_Atk);
        isAttack = false;
    }


    void Attack()
    {
        if (distance <= attackdistance && !isAttack)
        {
            isAttack = true;
            StartCoroutine(AttackCo());
        }
    }

    void OnHit(int damage)
    {
        StartCoroutine(OnHitCo(damage));
    }

    IEnumerator OnHitCo(int damage)
    {
        b_Hp -= damage;
        if (gamemanager.isSkill == false)
        {
            player.GetComponent<Player>().p_power += 5f;
            player.GetComponent<Player>().p_Hp += 5;
        }
        navagent.speed = 0;
        yield return new WaitForSeconds(0.5f);
        navagent.speed = patrolSpeed;
    }

    IEnumerator SoundCo()
    {
        int a = Random.RandomRange(0, 3);
        switch (a)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
        yield return new WaitForSecondsRealtime(1.5f);
    }

    bool CanSeePlayer()
    {
        if (playerTr == null)
            return false;

        Vector3 directionToPlayer = playerTr.position - transform.position;
        float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);

        // �÷��̾ �þ� ������ �þ� �� ���� �ִ��� Ȯ��
        if (directionToPlayer.magnitude <= viewRange && angleToPlayer <= viewAngle / 2f)
        {
            // �þ� ������ �þ� �� ���� �÷��̾ ����
            return true;
        }

        // �þ� ������ �þ� �� ���� �÷��̾ ����
        return false;
    }

    /*    private void OnDrawGizmos() //�߰ݹ��� �þ�
        {
            Handles.color = Color.red;
            Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, viewAngle / 2f, viewRange);
            Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -viewAngle / 2f, viewRange);
        }*/

    void HandleHP()
    {

        boss_hpbar.value = (float)b_Hp / (float)max_hp;

    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            isTrace = true;
            isBossHit = true;
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.damage);

        }
    }
}