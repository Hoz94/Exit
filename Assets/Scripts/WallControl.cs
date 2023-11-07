using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControl : MonoBehaviour
{
    Animator ani;
    public int id;
    private AudioSource wallaudio;
    public AudioClip wallsound;
    public int count = 0;

    void Awake()
    {
        ani = GetComponent<Animator>();    
        wallaudio = GetComponent<AudioSource>();
        wallaudio.clip = wallsound;
    }

    private void Start()
    {
        count = 0;
    }

    public void WallOpen()
    {
        switch(id)
        {
            case 0:
                ani.SetBool("D_Door_Down", true);

                if (wallaudio != null && count == 1)
                {
                    wallaudio.Play();

                }
                break;
        }
    }
}
