using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControl : MonoBehaviour
{
    Animator ani;
    public int id;
    private AudioSource wallaudio;
    public AudioClip wallsound;

    void Awake()
    {
        ani = GetComponent<Animator>();    
        wallaudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
       
    }

    public void WallOpen()
    {
        switch(id)
        {
            case 0:
                ani.SetBool("Start_Wall_Down", true);
                if(wallaudio!=null)
                    wallaudio.Play();
                break;
            case 1:
                ani.SetBool("IsDown", true);
                if (wallaudio != null)
                    wallaudio.Play();
                break;
            case 2:
                ani.SetBool("Key_Wall1_Down", true);
                if (wallaudio != null)
                    wallaudio.Play();
                break;
            case 3:
                ani.SetBool("Key_Wall3_Slide", true);
                if (wallaudio != null)
                    wallaudio.Play();
                break;
            case 4:
                ani.SetBool("Key_Wall4_Slide", true);
                if (wallaudio != null)
                    wallaudio.Play();
                break;
            case 5:
                ani.SetBool("Exit_Wall_Down", true);
                if (wallaudio != null)
                    wallaudio.Play();
                break;
            case 6:
                ani.SetBool("Tutorial_Wall", true);
                if (wallaudio != null)
                    wallaudio.Play();
                break;
        }
    }
}
