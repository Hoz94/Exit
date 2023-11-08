using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Area_Stay : MonoBehaviour
{
    public GameObject SpawnObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SpawnObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SpawnObject.SetActive(false);
    }
}
