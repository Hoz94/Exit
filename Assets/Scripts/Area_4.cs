using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_4 : MonoBehaviour
{
    public GameObject Area_3Trigger;
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
        Area_3Trigger.SetActive(false);
    }
}
