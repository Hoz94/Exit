using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Tuto_Mon : MonoBehaviour
{
    public static Tuto_Mon instance;
    private AudioSource TutorialAudio;
    public AudioClip TutorialMonsterSound;
    public int atk = 5;
    public int hp = 2500;
    public int maxhp = 2500;
    NavMeshAgent nav;
    Animator ani;
    public bool isHit;
    public Transform player;
    public float dist;
    public bool isAttack;
    public bool isTrace;
    public bool isdead;
    Vector3 dir;
    public float attackdelay;
    float attackinterval = 1.3f;
    Gamemanager gm;
    public GameObject tutorial_portal;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        TutorialAudio=GetComponent<AudioSource>();
        TutorialAudio.clip = TutorialMonsterSound;
        nav = GetComponent<NavMeshAgent>();
        ani=GetComponent<Animator>();
        gm = Gamemanager._instance;
    }

    // Update is called once per frame
    void Update()
    {

        HandleHP();
        if (!isAttack&&!isdead)
        {
            Trace();
        }

        if(hp<=0&&!isdead)
        {
            CapsuleCollider cc=this.gameObject.GetComponent<CapsuleCollider>();
            cc.enabled = false;
            StopCoroutine(AttackCo());
            
            nav.velocity = Vector3.zero;
            nav.angularSpeed = 0;
            StartCoroutine(DieCo());
            TutorialAudio.Stop();
        }

        

        dist = Vector3.Distance(transform.position, player.position);
    }

    void Trace()
    {
        if (isTrace)
        {
            nav.acceleration = 8;
            nav.speed = 5f;
            ani.SetBool("isTrace", true);
            dir = player.transform.position;
            nav.SetDestination(dir);

            if (dist <= 1.5f)
            {
                isAttack = true;
                isTrace = false;

                StartCoroutine(AttackCo());
            }
                                                                                                                
        }                                                                                                       
    }                                                                                                           
                                                                                                                
                                                                                                                
    IEnumerator AttackCo()
    {
        nav.velocity = Vector3.zero;
        nav.acceleration = 0;
        transform.LookAt(player.position);
        ani.SetBool("isTrace", false);
        ani.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.5f);
        player.gameObject.GetComponent<Player>().TakeDamage(atk);
        yield return new WaitForSeconds(attackinterval-0.5f);
        ani.SetBool("isTrace", true);
        ani.SetBool("isAttack", false);
        isAttack = false;
        isTrace = true;
    }

    IEnumerator DieCo()
    {
        isdead = true;
        ani.SetTrigger("isDead");
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        tutorial_portal.SetActive(true);
    }

    void OnHit(int damage)
    {
        hp -= damage;
        if(gm.isSkill==true)
        {
            player.GetComponent<Player>().p_Hp += 0.5f;
        }
    }


    void HandleHP()
    {
        slider.value = (float)hp / (float)maxhp;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            isTrace = true;
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.damage);
        }
    }
}
