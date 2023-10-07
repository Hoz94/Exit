using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    public Image Loadingbar;
    public Player player;
    public GameObject bloodscreen;


    [SerializeField]
    private Slider hpbar;
    private float MaxHp = 100f;

/*    [SerializeField]
    private Slider staminabar;
    public float MaxSta = 100f;*/

    [SerializeField]
    private Slider powerbar;
    private float Maxpower = 100f;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        Loadingbar.fillAmount = 0;
        bloodscreen.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
/*        HandleStamina();*/
        HandleKey();
        HandleHP();
        HandlePower();

    }

    public void HandleHP()
    {
        hpbar.value=player.p_Hp / MaxHp;
    }

/*    void HandleStamina()
    {
        staminabar.value = player.p_stamina / MaxSta;
    }*/

    void HandleKey()
    {
        Loadingbar.fillAmount = (float)Gamemanager._instance.CurKey / (float)Gamemanager._instance.MaxKey;
    }

    public IEnumerator ShowBloodScreen()
    {
        bloodscreen.SetActive(true);
        yield return new WaitForSeconds(0.7f);

        bloodscreen.SetActive(false);
    }

    void HandlePower()
    {
        powerbar.value = player.p_power / Maxpower;
    }
}