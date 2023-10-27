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
            // ������ �Ͻ� ���� ���� �� BGM�� �Ͻ� ����
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
        else
        {
            // ������ ���� ���� �� BGM�� ���
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}