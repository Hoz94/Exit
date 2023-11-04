using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject escui;

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


    public void OnClickResume() // 돌아가기
    {
        Time.timeScale = 1;
        escui.SetActive(false);
    }

    public void To_Lobby() // 로비로 돌아가기
    {
        SceneManager.LoadScene(0);
    }

    public void CallescUI() // ESC 눌렀을 때 UI 호출
    {
        escui.SetActive(true);
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
