using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class D_Portal : MonoBehaviour
{

    public GameObject ChatPanel;
    public Text ChatText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ChatPanel.SetActive(true);
        ChatText.text = "���踦 �� ��Ҵ�.\n���� ���������� �̵�����.";
        StartCoroutine(ChatCo());
    }

    IEnumerator ChatCo()
    {
        yield return new WaitForSeconds(2f);
        ChatPanel.SetActive(false);
    }
}