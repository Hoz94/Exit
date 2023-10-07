using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event : MonoBehaviour
{
    [SerializeField]
    private GameObject sliderObj;
    [SerializeField]
    private Slider openslider;
    [SerializeField]
    private GameObject door;

    public float Maxdoor = 100f;
    public float mindoor;
    
    bool isTrigger;
    bool isOpen;
    Animator ani;


    void Start()
    {
        ani=GetComponent<Animator>();
        mindoor = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTrigger)
        {
            sliderObj.SetActive(true);

            mindoor += 40f * Time.deltaTime;
            openslider.value = mindoor / Maxdoor;

            if (openslider.value >= 1f)
            {
                sliderObj.SetActive(false);
                ani.SetBool("LeverUp", true);
                WallControl wallControl = door.GetComponent<WallControl>();
                wallControl.WallOpen();
                isOpen = true;
            }
        }        

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = true;
            ani.SetBool("DoorOpen", true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        isTrigger = false;
        sliderObj.SetActive(false);
        if (!isOpen)
        {
            mindoor = 0f;
            ani.SetBool("DoorOpen", false);
        }

    }


}