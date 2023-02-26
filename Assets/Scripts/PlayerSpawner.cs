using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefab;
    public Transform[] playerZone;
    public float playerCount = 2;
    WaveSpawner waveSpawner;

    private void Awake()
    {
    }

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
            int zoneIndex = Random.Range(0, playerZone.Length);

            if (playerZone[zoneIndex].transform.childCount == 0)
            {
                var zoneObj = Instantiate(playerPrefab[index], playerZone[zoneIndex].transform.position, playerZone[zoneIndex].transform.rotation);
                zoneObj.transform.parent = playerZone[zoneIndex].transform;
                //playerPrefab[index].transform.SetParent(playerZone[zoneIndex].transform);
            }
            else
            {
                Debug.Log("컨티뉴함");

                continue;
            }
            yield return new WaitForSeconds(0.6f);

        }

    }
}
