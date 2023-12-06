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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("Trigger") || collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("TutorialGround"))
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            StartCoroutine(falseBullet());
        }       
    }
    IEnumerator falseBullet()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        this.gameObject.SetActive(false);
    }

}