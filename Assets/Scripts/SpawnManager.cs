using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header ("D���� ���� ������ƮǮ��")]
    public List<GameObject> MonsterPool = new List<GameObject>();
    public GameObject SpawnMonsterPrefab;
    int SpawnMonstersCount = 50;

    // Start is called before the first frame update
    void Start()
    {
        CreateMonsters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CreateMonsters() // ���� ����
    {
        GameObject MonsterPools = new GameObject("MonsterPools");

        for (int i = 0; i < SpawnMonstersCount; i++)
        {
            var obj = Instantiate(SpawnMonsterPrefab, MonsterPools.transform);
            obj.SetActive(false);
            MonsterPool.Add(obj);
        }
    }

    public GameObject GetMonsters() 
    {
        for (int i = 0; i < MonsterPool.Count; i++)
        {
            if (MonsterPool[i].activeSelf == false)
            {
                return MonsterPool[i];
            }
        }
        return null;
    }
}