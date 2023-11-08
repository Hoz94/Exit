using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class B_Area_Hint : MonoBehaviour
{
    public GameObject ChatPanel;
    public Text ChatText;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(count==5)
        {
            ChatPanel.SetActive(true);
            ChatText.text = "¸Ó¸´¼Ó¿¡ 4,1,4,2¶õ ¼ýÀÚ°¡ ¸âµ·´Ù.";
            StartCoroutine(ChatCo());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            count++;
        }

    }

    IEnumerator ChatCo()
    {
        yield return new WaitForSeconds(3f);
        count++;
        ChatPanel.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
