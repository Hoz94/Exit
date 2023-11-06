using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_5 : MonoBehaviour
{
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
        Area_4Trigger.SetActive(false);
    }
}
