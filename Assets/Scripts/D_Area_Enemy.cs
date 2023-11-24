using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class D_Area_Enemy : MonoBehaviour
{
    public static D_Area_Enemy _Instance;
    Transform player;
    public int MaxHP=200;
    public int HP=200;
    public float Atk=5f;
    private NavMeshAgent nvAgent;
    float nowSpeed=6f;
    private Vector3 dir;
    Animator _ani;
    public float dist;
    float Attackdist=2f;
    bool isTrace;
    [SerializeField] bool isDead;
    [SerializeField] bool isAttack;
    CapsuleCollider cc;
    public Slider Hp_Bar;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        nvAgent = GetComponent<NavMeshAgent>();
        _ani = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(this.gameObject.transform.position, player.position);

        if (isDead)
        {
            return;
        }

        if (HP <= 0)
        {
            cc.enabled = false;
            Death();
        }
        HandleHP();
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            if (!isAttack && dist != 0)
            {
                Trace();
            }
        }
    }

    void Trace()
    {
        _ani.SetBool("isTrace", true);
        _ani.SetBool("isAttack", false);
        nvAgent.angularSpeed = 5000000 * Time.deltaTime;
        nvAgent.acceleration = 8000000 * Time.deltaTime;
        nvAgent.speed = nowSpeed;
        dir = player.position;
        nvAgent.SetDestination(dir);
        if (dist <= Attackdist)
        {
            isAttack = true;
            transform.LookAt(dir);
            StartCoroutine(Attackco());
        }

    }

    IEnumerator Attackco()
    {
        _ani.SetBool("isAttack", true);
        _ani.SetBool("isTrace", false);
        player.gameObject.GetComponent<Player>().TakeDamage(Atk);
        nvAgent.speed = 0;
        nvAgent.velocity = Vector3.zero;
        nvAgent.acceleration = 0;
        nvAgent.angularSpeed = 0;
        yield return new WaitForSeconds(1.5f);
        _ani.SetBool("isTrace", true);
        _ani.SetBool("isAttack", false);
        isAttack = false;
    }

    void Death()
    {
        isDead = true;
        StartCoroutine(DeathCo());
    }

    IEnumerator DeathCo()
    {
        _ani.SetTrigger("isDead");
        nvAgent.speed = nowSpeed;
        nvAgent.velocity = Vector3.zero;
        nvAgent.acceleration = 0;
        nvAgent.angularSpeed = 0;

        yield return new WaitForSeconds(1f);
        nvAgent.angularSpeed = 5000000 * Time.deltaTime;
        nvAgent.acceleration = 8000000 * Time.deltaTime;
        nvAgent.speed = nowSpeed;
        cc.enabled = true;
        HP = MaxHP;
        isDead = false;
        _ani.SetBool("isTrace", true);
        this.gameObject.SetActive(false);
    }
    public void OnHit(int damage)
    {
        HP -= damage;

        player.GetComponent<Player>().p_power += 0.5f;
        player.GetComponent<Player>().p_Hp += 2f;
        
    }

    void HandleHP()
    {
        Hp_Bar.value = (float)HP / (float)MaxHP;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            isTrace = true;
            //isAttack = true;
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.damage);
        }
    }
}