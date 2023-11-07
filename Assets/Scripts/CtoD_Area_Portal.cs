using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CtoD_Area_Portal : MonoBehaviour
{
    public Player player;
    public GameObject D_Entrance;
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
        if(other.gameObject.CompareTag("Player"))
        {
            player.transform.position = D_Entrance.transform.position;
            StartCoroutine(ChatCo());
        }
    }

    IEnumerator ChatCo()
    {
        yield return new WaitForSeconds(1f);
        ChatPanel.SetActive(true);
        ChatText.text = "여긴 어디지..? 좋지 않은 기분이 든다.";
        yield return new WaitForSeconds(2f);
        ChatPanel.SetActive(false);
    }
}
