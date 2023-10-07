using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    private UI ui;
    public Player player;
    public bool isPortal = false;
    public bool ispaze3 = false;
    public GameObject ExitUI;
    public Gamemanager gamemanager;
    public Transform destination;
    public GameObject firstgoal;
    public GameObject secondgoal;
    public GameObject nextRound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPortal==true)
        {
            if(gamemanager.CurKey==gamemanager.MaxKey)
            {
                ispaze3 = true;
                player.transform.position = destination.position;
                Destroy(firstgoal.gameObject);
                secondgoal.SetActive(true);
                nextRound.SetActive(false);                
            }
            else if(gamemanager.CurKey!=gamemanager.MaxKey)
            {
                ExitUI.gameObject.SetActive(true);
            }
        }
        if(isPortal==false)
        {
            ExitUI.gameObject.SetActive(false);
        }

        
        
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPortal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isPortal = false;
    }
}
