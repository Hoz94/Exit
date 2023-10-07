using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class SpawnMonster : MonoBehaviour
{
    public Player player;

    public int Atk = 5;
    public int Hp = 30;
    NavMeshAgent navmesh;
    Animator anim;
    public float attackdist;
    public float dist;
    bool iswalk;
    bool isattack;
    public Vector3 dir;
    int itemnum;
    public GameObject Hppotion;
    public GameObject Powerpotion;
    Rigidbody rb;

    void Awake()
    {
        rb= GetComponent<Rigidbody>();
        navmesh=GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        dist =Vector3.Distance(transform.position, player.transform.position);
  
        Trace();
        
    }

    void Trace()
    {
        
        iswalk =true;
        dir = player.transform.position;
        navmesh.SetDestination(dir);
        navmesh.speed = 3f;
        if(dist<=attackdist&&!isattack)
        {
            iswalk = false;
            isattack = true;
            Attack();
        }
    }

    void Attack()
    {
        StartCoroutine(Attackco());

    }

    IEnumerator Attackco()
    {
        player.TakeDamage(Atk);
        anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isAttack", false);
        isattack = false;
        iswalk = true;
    }

    IEnumerator OnHitCo(int damage)
    {

        Hp -= damage;
        if(Hp<=0)
        {
            navmesh.velocity = Vector3.zero;
            navmesh.acceleration = 0;

            anim.SetTrigger("isDead");
            this.GetComponent<Collider>().enabled = false;
            DropItem();
           

        }
        yield return new WaitForSeconds(0.5f);
    }

    void OnHit(int damage)
    {
        StartCoroutine(OnHitCo(damage));
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.damage);
        }
    }

    void DropItem()
    {
        int ran = Random.Range(0, 10);
        Vector3 vec = new Vector3(0, -0.5f, 0);
        if (ran < 7)
        {
            
        }

        else if (ran < 9)
        {
            Instantiate(Hppotion, transform.position+vec, transform.rotation);
        }
        else if (ran < 10)
        {
            Instantiate(Powerpotion, transform.position+vec,transform.rotation);
        }
        StartCoroutine(destroyMon());
        
    }

    IEnumerator destroyMon()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

}