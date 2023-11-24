using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour
{
    public GameObject Explanation;
    public Image gameImage;
    public float fadeSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Explanation.SetActive(false);
        SetImageAplha(0f);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnClickStartGame()
    {
        Explanation.SetActive(true);
        StartCoroutine(FadeInImage());
        Debug.Log("게임스타트");
    }

    public void OnClickRealStartGame()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator FadeInImage()
    {
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += fadeSpeed * Time.deltaTime;
            SetImageAplha(alpha);
            yield return null;
        }
    }

    void SetImageAplha(float alpha)
    {
        Color imageColor = gameImage.color;
        imageColor.a = alpha;
        gameImage.color = imageColor;
    }


    public void OnclickQuit()
    {
        Application.Quit();
    }

}
