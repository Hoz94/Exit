using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject EscUI;
    public GameObject FlashUI;
    public GameObject GunExplationUI;
    public GameObject B_Area_Explanation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscUI.SetActive(true);
            Time.timeScale = 0;
        }
    }


    public void OnClickResume() // 돌아가기
    {
        Time.timeScale = 1;
        EscUI.SetActive(false);
    }

    public void OnFlashResume() // 후레쉬 습득하고 UI창 나온거 닫기, 총 설명 UI창 닫기
    {
        Time.timeScale = 1;
        FlashUI.SetActive(false);
        GunExplationUI.SetActive(false);

    }

    public void OnArea_B_ExplanationResume() // B구역 설명 끄기
    {
        Time.timeScale = 1;
        B_Area_Explanation.SetActive(false);
    }

    public void To_Lobby() // 로비로 돌아가기
    {
        SceneManager.LoadScene(0);
    }

    public void CallescUI() // ESC 눌렀을 때 UI 호출
    {

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
