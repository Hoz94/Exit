using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialZombie : MonoBehaviour
{
    public Animator _ani;
    int Hp = 30;
    // Start is called before the first frame update
    void Start()
    {
        _ani=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            _ani.SetTrigger("isDead");
            Destroy(this.gameObject,1.5f);
        }
    }
}
