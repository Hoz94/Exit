using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_2 : MonoBehaviour
{
    public GameObject Area_1Trigger;
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
        Area_1Trigger.SetActive(false);
    }
}
