using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class BlockingArea : MonoBehaviour
{
    public GameObject ChatPanel;
    public Text ChatText;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ChatPanel.activeSelf&&count==1)
        {
            StartCoroutine(ChatCo());
        }
    }

    IEnumerator ChatCo()
    {
        yield return new WaitForSeconds(3f);
        ChatPanel.SetActive(false);
        count = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            count++;
            ChatPanel.SetActive(true);
            ChatText.text = "알 수 없는 기운이 몸을 막는다.";

        }
    }

}
