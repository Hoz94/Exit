using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            // 게임이 일시 정지 중일 때 BGM을 일시 정지
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
        else
        {
            // 게임이 실행 중일 때 BGM을 재생
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
