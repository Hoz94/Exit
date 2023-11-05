using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Trigger : MonoBehaviour
{
    public GameObject TutorialZombie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            TutorialZombie.SetActive(true);
            
            this.gameObject.SetActive(false);
        }
    }
}
