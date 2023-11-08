using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public SpawnManager SpawnManager;
    public Transform[] spawnPoints;
    public float spawnInterval = 1f;

    private float spawnTimer = 1f;

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnInterval)
        {
            SpawnMonster();
            spawnTimer = 0f;
        }
    }

    void SpawnMonster()
    {
        int randomIndex=Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        GameObject monster = SpawnManager.GetMonsters();

        if(monster != null ) 
        {
            monster.transform.position = spawnPoint.position;
            monster.transform.rotation = spawnPoint.rotation;
            monster.SetActive(true);
        }
    }
}
