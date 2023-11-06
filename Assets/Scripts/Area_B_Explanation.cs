using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_B_Explanation : MonoBehaviour
{
    public GameObject AreaB_Explanation;
    int ExplanationCount = 0;
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
        if (other.CompareTag("Player"))
        {
            
            if (ExplanationCount == 0)
            {
                Time.timeScale = 0;
                AreaB_Explanation.SetActive(true);
                ExplanationCount++;
            }
        }
    }
}
