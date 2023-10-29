using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Gamemanager : MonoBehaviour
{
    [Header("권총 총알 오브젝트풀링")]
    public GameObject bullet1Prefab;
    public int bullet1maxPool = 5;
    public List<GameObject> bullet1Pool = new List<GameObject>();
    [Header("기관총 총알 오브젝트풀링")]
    public GameObject bullet2Prefab;
    public int bullet2maxPool = 30;
    public List<GameObject> bullet2Pool = new List<GameObject>();

    public Player player;
    public BoxOpen bx;
    public GameObject camera;
    public static Gamemanager _instance;
    public GameObject gameOverUI;
    public Image[] BulletImg;
    public GameObject wp1;
    public GameObject wp2;
    public Button skillbutton;
    public int MaxKey = 4;
    public int CurKey=0;
    public GameObject Exit;
    public GameObject escui;
    public Portal portal;
    public bool isSkill = false;
    public bool isStart = false;
    public Text nexttxt;
    public GameObject skilltxt;
    public bool skillready;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        CreateBullet1Pooling();
        CreateBullet2Pooling();
    }

    private void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            CallescUI();
        }

        if (player.p_Hp<=0)
        {
            GameOver();
           
        }

        if (player.p_power >= 100 && isSkill==false)
        {
            skillbutton.gameObject.SetActive(true);
            //skilltxt.SetActive(true);
            
            if(Input.GetKeyDown (KeyCode.Q)) 
            {
                //skilltxt.SetActive(false);
                OnSkill();
            }
        }

        if (isSkill==true)
        {
            if (Input.GetMouseButton(0))
            {
                skillready = true;
                player.p_power -= 10f * Time.deltaTime;
                if (player.p_power <= 0)
                {
                    player.p_power = 0;
                    wp1.SetActive(true);
                    wp2.SetActive(false);
                    isSkill = false;
                    skillbutton.gameObject.SetActive(false);
                }
            }
        }

    }

    public void CreateBullet1Pooling()
    {
        GameObject object1Pools = new GameObject("ObjectPools");

        for (int i = 0; i < bullet1maxPool; i++)
        {
            var obj = Instantiate(bullet1Prefab, object1Pools.transform);
            obj.name = "Bullet_" + i.ToString("00");
            obj.SetActive(false);
            bullet1Pool.Add(obj);
        }
    }

    public void CreateBullet2Pooling()
    {
        GameObject object2Pools = new GameObject("SkillObjectPools");

        for (int i = 0; i < bullet2maxPool; i++)
        {
            var obj = Instantiate(bullet2Prefab, object2Pools.transform);
            obj.name = "Bullet_" + i.ToString("00");
            obj.SetActive(false);
            bullet2Pool.Add(obj);
        }
    }

    public GameObject GetBullet1()
    {
        for (int i = 0; i < bullet1Pool.Count; i++)
        {
            if (bullet1Pool[i].activeSelf == false)
            {
                return bullet1Pool[i];
            }
        }

        return null;
    }

    public GameObject GetBullet2()
    {
        for (int i = 0; i < bullet2Pool.Count; i++)
        {
            if (bullet2Pool[i].activeSelf == false)
            {
                return bullet2Pool[i];
            }
        }

        return null;
    }

    public void OnSkill()
    {
        wp1.SetActive(false);
        wp2.SetActive(true);
        skillbutton.gameObject.SetActive(false);
        isSkill = true;
    }

    public void To_Lobby()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }


    public void GameOver()
    {
        Time.timeScale = 0;
        camera.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOverUI.SetActive(true);
        player.gameObject.SetActive(false);
    }
    public void bulletminus()
    {
        for(int i = 0; i < BulletImg.Count(); i++)
        {
            if (BulletImg[i].IsActive())
            {
                BulletImg[i].enabled = false;
                break;
            }
        }
    }
    public void bulletReload()
    {
        for (int i = 0; i < BulletImg.Count(); i++)
        {
            BulletImg[i].enabled = true;
        }
    }



    public void OnClickResume()
    {
        Time.timeScale = 1;
        escui.SetActive(false);
        player.gameObject.SetActive(true);
        camera.SetActive(false);
        
    }

/*    public void OnClickQuit()
    {
        Application.Quit();
    }*/


    public void CallescUI()
    {
        escui.SetActive(true);
        Time.timeScale = 0;
        player.gameObject.SetActive(false);
        camera.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
    }
}