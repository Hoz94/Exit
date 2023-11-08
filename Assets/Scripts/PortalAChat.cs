using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PortalAChat : MonoBehaviour
{
    public GameObject ChatPanel;
    public Text Chattext;
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
        if(other.gameObject.CompareTag("Player"))
        {
            ChatPanel.SetActive(true);
            Chattext.text = "포탈을 타면 시작점으로 돌아오는것 같다";
            StartCoroutine(ChatCo());
        }
    }

    IEnumerator ChatCo()
    {
        yield return new WaitForSeconds(2f);
        ChatPanel.SetActive(false);
    }
}
