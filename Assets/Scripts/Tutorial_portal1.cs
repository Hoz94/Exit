using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_portal1 : MonoBehaviour
{
    bool isPaze2;
    public GameObject firstGoal;
    public GameObject tuto_goal;
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
            isPaze2 = true;
            if (isPaze2)
            {
                firstGoal.SetActive(true);
                tuto_goal.SetActive(false);
            }
        }
    }
}
