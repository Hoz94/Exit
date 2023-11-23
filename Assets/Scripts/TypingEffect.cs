using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    public GameObject StartBtn;
    public Text text;
    private string m_text = "������ �Ĺ���ȭ ��Ű�� ���� �ٸ� ���Ͽ� �����ϴ� \r\n\r\n�����༺�� �εμ��簡 �ڽ��� �༺���� ���� ����� \r\n\r\n�ּ���Ż�� Ȱ���� ������� ������ ħ�����׽��ϴ�.\r\n\r\n�� ��Ż�� �������� �ּ� ������ ������ �ǰ� �ʿ��մϴ�.\r\n\r\n�εμ��縦 óġ�ϰ� ������ ȹ���ϼ���.\r\n\r\n���� ���� �� ESC�� ���� �ӹ��� ������ �� �� �ֽ��ϴ�.";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(texteffect());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator texteffect()
    {
        yield return new WaitForSeconds(2f);
        for(int i=0; i<=m_text.Length; i++)
        {
            text.text=m_text.Substring(0,i);
            yield return new WaitForSeconds(0.07f);
        }
        StartBtn.SetActive(true);
    }
}