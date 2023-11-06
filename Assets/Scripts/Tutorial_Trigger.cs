using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Trigger : MonoBehaviour
{
    public GameObject GunExplation;
    public GameObject GunMode;
    public GameObject CrossHair;
    public GameObject BulletImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 50f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GunMode.SetActive(true);
            CrossHair.SetActive(true);
            BulletImage.SetActive(true);
            Time.timeScale = 0f;
            GunExplation.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
