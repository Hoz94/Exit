using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_1 : MonoBehaviour
{
    public GameObject Area_1Trigger;
    public GameObject Area_2Trigger;
    public GameObject Area_3Trigger;
    public GameObject Area_4Trigger;
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
        Area_1Trigger.SetActive(true);
        Area_2Trigger.SetActive(true);
        Area_3Trigger.SetActive(true);
        Area_4Trigger.SetActive(true);
    }
}
