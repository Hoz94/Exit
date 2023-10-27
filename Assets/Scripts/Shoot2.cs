using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Shoot2 : MonoBehaviour
{
    private AudioSource Theaudio;
    public AudioClip shootsound;
    public GameObject Bullet;
    public Transform BulletPos;
    public Gamemanager gamemanager;

    public Player player;

    public float MaxDelay;
    public float MinDelay;
    public float CurrenDelay = 0f;

    public bool isFire;
    // Start is called before the first frame update
    void Start()
    {
        Theaudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamemanager.skillready == true)
        {           
            MinDelay += Time.deltaTime;
            Fire();
        }

    }

    void Fire()
    {
        isFire = true;
        if (Input.GetMouseButton(0))
        {
            if (MinDelay > MaxDelay)
            {
                var _bullet = Gamemanager._instance.GetBullet2();
                if (_bullet != null)
                {
                    Theaudio.Play();
                    _bullet.transform.position = BulletPos.position;
                    _bullet.transform.rotation = BulletPos.rotation;
                    _bullet.SetActive(true);
                    MinDelay = CurrenDelay;
                }
            }
        }
        gamemanager.skillready = false;

    }

    private void OnDisable()
    {
        isFire = false;
    }
}