using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject EscUI;
    public GameObject FlashUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CallescUI();
        }
    }


    public void OnClickResume() // ���ư���
    {
        Time.timeScale = 1;
        EscUI.SetActive(false);
    }

    public void OnFlashResume() // �ķ��� �����ϰ� UIâ ���°� �ݱ�
    {
        Time.timeScale = 1;
        FlashUI.SetActive(false);
    }

    public void To_Lobby() // �κ�� ���ư���
    {
        SceneManager.LoadScene(0);
    }

    public void CallescUI() // ESC ������ �� UI ȣ��
    {
        EscUI.SetActive(true);
        Time.timeScale = 0;
    }


    public void HandleMouseSpeed()
    {

    }

    public void HandleBGMVolume()
    {

    }

    public void HandleEffectVolume()
    {

    }
}