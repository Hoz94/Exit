using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    public GameObject StartBtn;
    public Text text;
    private string m_text = "지구를 식민지화 시키기 위해 다른 은하에 존재하는 \r\n\r\n좀비행성의 부두술사가 자신의 행성에서 좀비를 만들어 \r\n\r\n주술포탈을 활용해 좀비들을 지구로 침공시켰습니다.\r\n\r\n이 포탈을 닫으려면 주술 시전자 심장의 피가 필요합니다.\r\n\r\n부두술사를 처치하고 심장을 획득하세요.\r\n\r\n게임 진행 중 ESC를 눌러 임무와 지도를 볼 수 있습니다.";

    
    IEnumerator texteffect()
    {
        yield return new WaitForSecondsRealtime(2f);
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(0.03f); // waitforseconds 최적화 작업. 검색해봐야 함.
        for (int i=0; i<=m_text.Length; i++)
        {
            text.text=m_text.Substring(0,i);
            yield return wait;
        }
        StartBtn.SetActive(true);
    }

    private void OnEnable()
    {
        StartCoroutine(texteffect());
    }
}