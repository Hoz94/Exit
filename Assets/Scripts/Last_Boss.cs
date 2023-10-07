using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Last_Boss : MonoBehaviour
{
    [SerializeField]
    private Slider BSlider;

    public Portal portal;
    public int max_Hp = 3000;
    public int B_Hp = 3000;
    public float B_Atk = 30f;
    public int patrolSpeed = 3;
    public Transform playerTr;
    public Player player;
    public Gamemanager gamemanager;
    public GameObject B_Hp_Bar;
    public GameObject healing;
    public GameObject enemy;
    public Transform[] spawnpoints;
    public GameObject skill;
    private GameObject activeskill;
    
    bool isNearAttack; //근접공격
    bool isFarAttack; //원거리공격
    bool isHit; //피격
    bool isRoar; //그 자리에서 붕뜸(체력회복 or 몹 소환)


    bool isWalk; // 걷기
    bool isDead; //죽음
    bool isSkill;


    public float farcool;
    public float attackdist = 2.5f;
    public float dist;
    public float viewRange;
    public float viewAngle;
    public float roartime;
    public float skillcool;
    public float hitrange;
    public float skilldmg=30f;
    private NavMeshAgent navagent;
    Animator animator;
    Vector3 dir;
    Vector3 FarAtkPoint;

    private void Awake()
    {
        navagent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        B_Hp_Bar.gameObject.SetActive(false);
        isWalk = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanSeePlayer()&&portal.ispaze3==true)
        {
            B_Hp_Bar.gameObject.SetActive(true);
        }

        if (CanSeePlayer() && !isNearAttack&& !isFarAttack&&!isRoar&&!isSkill)
        {
            Trace();

        }
        dist =Vector3.Distance(transform.position, playerTr.position);
        hitrange = Mathf.Abs((player.transform.position - FarAtkPoint).magnitude);

        if(isHit==true)
        {
            HandleHP();
            
        }

        Vector3 playerposition = player.transform.position;
        
        roartime += Time.deltaTime;
        skillcool += Time.deltaTime;
        farcool += Time.deltaTime;

        if(B_Hp<=0)
        {
            BSlider.gameObject.SetActive(false);
        }

    }

    void Trace()
    {
        if (isWalk == true)
        {
            navagent.speed = 4f;
            navagent.acceleration = 8;
            animator.SetBool("isWalk", true);


            dir = playerTr.position;
            navagent.SetDestination(dir);
            if(!isRoar)
            {
                Attack();
            }
        }
        if (BSlider.value <= 0.7 && roartime >= 30f&&!isSkill&&!isFarAttack&&!isNearAttack)
        {
            Roar();
        }



    }
    IEnumerator Skillco()
    {
        navagent.velocity=Vector3.zero;
        navagent.acceleration = 0;
        isWalk = false;
        animator.SetBool("isSkill", true);
        
        yield return new WaitForSeconds(1.5f);
        Skill(playerTr.position);
        player.TakeDamage(skilldmg);
        yield return new WaitForSeconds(3f);
        isSkill = false;
        isWalk = true;
        
        animator.SetBool("isSkill", false);
        animator.SetBool("isWalk", true);
    }
    void Skill(Vector3 targetposition)
    {
        skillcool = 0f;
        
        Vector3 skillposition=new Vector3(playerTr.position.x,0,playerTr.position.z);
        activeskill=Instantiate(skill, skillposition, Quaternion.identity);
        Destroy(activeskill, 2.5f);
    }

    void Attack()
    {
        if(dist<=attackdist)
        {
            
            isNearAttack = true;
            Near_Attack();
            
        }
        else if (7f > dist && dist > attackdist&&farcool>=6f)
        {
            isFarAttack = true;
            Far_Attack();

        }

        else if (skillcool >= 15f&&!isRoar && !isFarAttack && !isNearAttack&&BSlider.value<=0.9f)
        {
            isSkill = true;
            transform.LookAt(player.transform.position);
            StartCoroutine(Skillco());
        }
    }

    void Near_Attack()
    {
        B_Atk = 30f;
        isWalk = false;
        StartCoroutine(NearAttack());
        
    }

    IEnumerator NearAttack()
    {
        navagent.acceleration = 0;
        navagent.velocity = Vector3.zero;
        animator.SetBool("isWalk", false);
        animator.SetBool("isAttack1", true);

        yield return new WaitForSeconds(0.7f);
        player.TakeDamage(B_Atk);
        yield return new WaitForSeconds(1.3f);
        animator.SetBool("isAttack1", false);
        animator.SetBool("isWalk", true);
        isNearAttack = false;
        isWalk = true;
    }
    void Far_Attack()
    {
        B_Atk = 5f;
        isFarAttack = true;
        isWalk = false;

        transform.LookAt(player.transform.position);
        FarAtkPoint= transform.position + (transform.forward * 5f);
        StartCoroutine(FarAttackco());        
    }
    IEnumerator FarAttackco()
    {
        navagent.acceleration = 0;
        navagent.velocity = Vector3.zero;
        
        animator.SetBool("isWalk", false);
        animator.SetBool("isAttack2", true);
        yield return new WaitForSeconds(0.8f);
        for(int i=0; i<7;i++)
        {
            if (hitrange <= 2.5f)
            {
                player.TakeDamage(B_Atk);
            }
            yield return new WaitForSeconds(0.2f);
        }
        animator.SetBool("isAttack2", false);
        animator.SetBool("isWalk", true);
        isFarAttack = false;
        isWalk = true;
        farcool = 0f;
    }
    void Hit(int damage)
    {
        StartCoroutine(OnHitCo(damage));
    }
    IEnumerator OnHitCo(int damage)
    {

        B_Hp -= damage;
        
        
        
        if (gamemanager.isSkill == false)
        {
            player.GetComponent<Player>().p_Hp += 5f;
            player.GetComponent<Player>().p_power += 10f;
            navagent.velocity = Vector3.zero;
            navagent.acceleration = 0;
        }

        if(gamemanager.isSkill==true)
        {
            player.GetComponent<Player>().p_Hp += 0.5f;
        }
        

        if (B_Hp <= 0)
        {
            isDead = true;
            animator.SetTrigger("isDead");
            navagent.velocity = Vector3.zero;
            Destroy(gameObject, 1.5f);

        }
        
        yield return new WaitForSeconds(0.8f);


    }
    void Roar()
    {
        isRoar = true;
        B_Atk = 0;
        StartCoroutine(Roarco());
        roartime = 0;
    }

    IEnumerator Roarco()
    {
        navagent.acceleration = 0;
        navagent.velocity = Vector3.zero;
        animator.SetBool("isRoar", true);
        yield return new WaitForSeconds(1f);
        SpawnEnemies();
        healing.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        B_Hp += 500;
        isRoar = false;
        healing.gameObject.SetActive(false);
        animator.SetBool("isRoar", false);
    }
    void SpawnEnemies()
    {
        for(int i=0; i<spawnpoints.Length;i++)
        {
            Instantiate(enemy, spawnpoints[i].position, Quaternion.identity);
        }
    }
    bool CanSeePlayer()
    {
        if (playerTr == null)
            return false;

        Vector3 directionToPlayer = playerTr.position - transform.position;
        float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);

        // 플레이어가 시야 범위와 시야 각 내에 있는지 확인
        if (directionToPlayer.magnitude <= viewRange && angleToPlayer <= viewAngle / 2f)
        {
            isWalk = true;
            // 시야 범위와 시야 각 내에 플레이어가 있음
            return true;
        }

        // 시야 범위와 시야 각 내에 플레이어가 없음
        return false;
    }


/*    private void OnDrawGizmos() //추격범위 시야
    {
        Handles.color = Color.red;
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, viewAngle / 2f, viewRange);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -viewAngle / 2f, viewRange);
    }
*/




    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            isHit = true;
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            Hit(bullet.damage);
                
        }
    }


    void HandleHP()
    {
        BSlider.value = (float)B_Hp / (float)max_Hp;
        
    }


}