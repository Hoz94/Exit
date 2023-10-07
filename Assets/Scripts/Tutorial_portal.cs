using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_portal : MonoBehaviour
{
    public Transform nextdest;

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
            Vector3 asd = new Vector3(10, 0, 0);
            other.transform.position = nextdest.position;
            other.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}
