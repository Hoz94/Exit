using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    public GameObject Flashobj;
    public GameObject FlashUI;
    // Start is called before the first frame update
    void Start()
    {
        Flashobj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * 50f * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Flashobj.SetActive(true);
            FlashUI.SetActive(true);
            Time.timeScale = 0f;
            Destroy(this.gameObject);
        }
    }
}
