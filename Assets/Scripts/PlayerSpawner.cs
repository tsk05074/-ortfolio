using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefab;
    public float playerCount = 2;
    WaveSpawner waveSpawner;
    
    void Start()
    {
        waveSpawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        StartCoroutine(SpawnPlayer());
    }

    void Update()
    {
    }

    public void WavePlayer()
    {
        if (waveSpawner.waveEnd <= 0)
        {
            StartCoroutine(SpawnPlayer());
        }
    }

    public IEnumerator SpawnPlayer()
    {
        
        for (int i = 0; i < playerCount; i++)
        {
            int index = Random.Range(0, 3);
            int XRange = Random.Range(5, 8);
            int YRange = Random.Range(-4, 3);

            Instantiate(playerPrefab[index], new Vector2(XRange, YRange), transform.rotation);
            yield return new WaitForSeconds(0.6f);
        }
    }
}
