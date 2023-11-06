using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Area_B_Trigger : MonoBehaviour
{
    public Player player;
    public Transform ReturnToArea;

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
        if(other.CompareTag("Player"))
        {
            player.transform.position=ReturnToArea.position;
        }
    }

}
