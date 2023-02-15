using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float enemyCount = 20f;
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        for (int i=0;i<enemyCount;i++)
        {
            Instantiate(enemyPrefab, enemyPrefab.transform.position, transform.rotation);
            yield return new WaitForSeconds(0.6f);
            Debug.Log(enemyCount);
        }
    }

}
