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
    public GameObject PotionExplanation;
    public GameObject MapExplanation;
    public GameObject MissionText;
    public GameObject RealToLobby;

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
            MapExplanation.SetActive(false);
            MissionText.SetActive(false);
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
        RealToLobby.SetActive(true); // 정말로 돌아갈거냐? 화면 띄우기
    }

    public void Real_Go_To_Lobby() // 예 누르면 로비로 보내기
    {
        SceneManager.LoadScene(0);
    }

    public void LobbyNo() // 로비로 돌아가지 않는다는 버튼
    {
        RealToLobby.SetActive(false);
    }

    public void OnQuitPotionExplanation()
    {
        Time.timeScale = 1;
        PotionExplanation.SetActive(false);
    }

    public void ClickMissionUI()
    {
        MapExplanation.SetActive(false);
        MissionText.SetActive(true);
    }

    public void MapExplanationUI()
    {
        MissionText.SetActive(false);
        MapExplanation.SetActive(true);
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
