using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    ParticleSystem ps;
    public GameObject ExitText;
    public GameObject Exitportal;
    public GameObject ExitUi;
    public Last_Boss lastboss;
    bool isget;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(lastboss==null)
        {
            lastboss.B_Hp = 0;
            lastboss.B_Hp_Bar.SetActive(false);
            ExitText.gameObject.SetActive(true);
            Exitportal.gameObject.SetActive(true);
            if (isget==true)
            {
                Quit();
            }
        }
        
    }

    void Quit()
    {
        ExitText.gameObject.SetActive(false);
        ExitUi.gameObject.SetActive(true);
        StartCoroutine(Quitco());
    }

    IEnumerator Quitco()
    {
        
        yield return new WaitForSeconds(4f);
        Application.Quit();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isget = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isget = false;
    }

}