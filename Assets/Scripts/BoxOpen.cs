using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxOpen : MonoBehaviour
{
    bool isget;
    [SerializeField]
    private Slider itembar;
    public float Maxitembar = 100f;
    public float curitembar = 0f;
    public Text text;
    bool TextOn;
    public bool nvOn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isget == true)
        {
            itembar.gameObject.SetActive(true);

            curitembar += 30f * Time.deltaTime;
            itembar.value = curitembar / Maxitembar;

            if (itembar.value >= 1f)
            {
                itembar.gameObject.SetActive(false);
                Destroy(gameObject);
                text.gameObject.SetActive(true);
                TextOn = true;
                if(TextOn==true)
                {
                    nvOn = true;
                    Destroy(text, 2.5f);
                }
            }
            
        }
        else
        {  
            itembar.gameObject.SetActive(false);
            curitembar = 0f;
        }

    }


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isget = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isget = false;
    }
}