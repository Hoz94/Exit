using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    public GameObject Flashobj;
    public GameObject Textobj;

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


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Flashobj.SetActive(true);
            Destroy(this.gameObject);
            Textobj.SetActive(true);
            Destroy(Textobj, 1.5f);
        }
    }
}
