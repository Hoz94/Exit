using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    private AudioSource Theaudio;
    public AudioClip shootsound;
    private AudioSource Reloadaudio;
    public AudioClip Reloadsound;
    public GameObject Bullet;
    public Transform BulletPos;
    public Text ReloadingText;
    public int MaxBullCount = 5;
    public int MinBullCount = 0;
    public Gamemanager gm;
    public float MaxDelay;
    public float MinDelay;
    public float CurrenDelay = 0f;
    public float Reloaddelay;
    private Animator _ani;
    float reloadtime;

    
    void Start()
    {
        _ani = GetComponentInChildren<Animator>();
        Theaudio = GetComponent<AudioSource>();
        Reloadaudio = gameObject.AddComponent<AudioSource>();
        Reloadaudio.clip = Reloadsound;
    }

    private void OnEnable()
    {
        transform.Rotate(0f, -1.668f, 0f);
    }
    // Update is called once per frame
    void Update()
    {
        Fire();
        MinDelay += Time.deltaTime;
        if(MaxBullCount==0)
        {
            Reload();
        }
    }

    public void Fire() // 사격
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            if (MinDelay > MaxDelay)
            {
                if (MaxBullCount > MinBullCount)
                {
                    var _bullet = Gamemanager._instance.GetBullet1();
                    if (_bullet != null)
                    {
                        Theaudio.Play();
                        _bullet.transform.position = BulletPos.position;
                        _bullet.transform.rotation = BulletPos.rotation;
                        _bullet.SetActive(true);
                        MaxBullCount--;
                        Gamemanager._instance.bulletminus();
                    }
                    MinDelay = CurrenDelay;
                    _ani.SetTrigger("isShot");
                }
            }
        }

    }

    public void Reload() // 장전
    {
        reloadtime += Time.deltaTime;
        ReloadingText.gameObject.SetActive(true);
        Reloaddelay += Time.deltaTime;
        if (Reloaddelay >= 1)
        {

            MaxBullCount = 5;
            Gamemanager._instance.bulletReload();
            Reloaddelay = 0;
            if (reloadtime >= 1)
            {
                Reloadaudio.Play();
                ReloadingText.gameObject.SetActive(false);
                reloadtime = 0f;
            }
        }
    }

    private void OnDisable()
    {
        ReloadingText.gameObject.SetActive(false);
    }
}