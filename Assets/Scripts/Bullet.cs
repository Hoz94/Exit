using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float BulSpeed;
    Ray ray;
    Vector3 dir;
    private Transform tr;
    private Rigidbody rb;



    private void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

    }

    private void OnEnable()
    {
        rb.AddForce(transform.forward * BulSpeed);
    }

    private void OnDisable()
    {
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        rb.Sleep();
    }

    // Start is called before the first frame update
    void Start()
    {
        
/*        dir = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        Destroy(this.gameObject, 3f);*/
    }

    // Update is called once per frame
    void FixedUpdate()
    {
/*        BulletPos = GameObject.FindWithTag("BulletPos").transform;
        ray = new Ray(BulletPos.position, dir);
        transform.position += transform.forward* BulSpeed * Time.deltaTime;*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("Trigger") || collision.gameObject.CompareTag("Ground"))
        {
            /*Destroy(this.gameObject);*/
            this.gameObject.SetActive(false);
        }

        else
        {
            StartCoroutine(falseBullet());
            
        }       
    }
    IEnumerator falseBullet()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
    }

}