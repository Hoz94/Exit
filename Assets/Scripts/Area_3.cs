using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_3 : MonoBehaviour
{
    public GameObject Area_2Trigger;
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
        Area_2Trigger.SetActive(false);
    }
}
