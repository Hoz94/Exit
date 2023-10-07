using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public Gamemanager gm;
    public GameObject Explation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Explation.SetActive(false);
        }
    }


    public void OnClickNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickExplation()
    {
        Explain();

    }

    public void OnclickQuit()
    {
        Application.Quit();
    }

    public void Explain()
    {
        Explation.SetActive(true);
    }

}
