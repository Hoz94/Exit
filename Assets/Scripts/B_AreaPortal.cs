using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class B_AreaPortal : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            ChatPanel.SetActive(true);
            ChatText.text = "정말 이상한 곳이야..\n얼른 탈출하고 싶군..";
            StartCoroutine(ChatCo());
        }
    }

    IEnumerator ChatCo()
    {
        yield return new WaitForSeconds(2f);
        ChatPanel.SetActive(false);
    }
}
