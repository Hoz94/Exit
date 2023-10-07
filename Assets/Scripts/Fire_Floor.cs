using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fire_Floor : MonoBehaviour
{
    public Player player;
    public float damage;
    bool isstay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        damage = 7.5f * Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == ("Player"))
        {
            isstay = true;
            player.TakeDamage(damage);

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isstay = false;
    }

}