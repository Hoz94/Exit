using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    private Slider key_slider;
    [SerializeField]
    private GameObject key_slider_obj;

    public float max_value = 100f;
    public float min_value;
    bool _isGet;
    bool isHave;
    public string type;
    // Start is called before the first frame update
    void Start()
    {
        min_value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        /*        ItemGet();*/
        TouchGet();
        if (type == "key")
        {
            transform.Rotate(Vector3.right * 50f * Time.deltaTime);
        }
    }


    void TouchGet()
    {
        if(_isGet==true)
        {
            key_slider_obj.SetActive(true);
            min_value += 80f * Time.deltaTime;
            key_slider.value = min_value / max_value;

            if(key_slider.value>=1f)
            {
                key_slider_obj.SetActive(false);
                Destroy(gameObject);
                Gamemanager._instance.CurKey++;
                isHave = true;
            }
        }
    }

/*    void ItemGet() // 키를 눌러 습득
    {
        if (_isGet == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(gameObject);
                Gamemanager._instance.CurKey++;
            }
        }
    }*/

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isGet = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        _isGet = false;
        key_slider_obj.SetActive(false);
        if (!isHave)
        {
            min_value = 0f;
        }
    }
}