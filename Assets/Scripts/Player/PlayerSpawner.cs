using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefab;
    public Transform[] playerZone;
    public float playerCount = 2;
    [SerializeField]
    private WaveSpawner enemySpawner;

    private void Awake()
    {

    }

    void Start()
    {
        PlayerSpawner playerspawner = gameObject.GetComponent<PlayerSpawner>();
        playerspawner.StartCoroutine(SpawnPlayer());

    }

    public IEnumerator SpawnPlayer()
    {
        while (true)
        {
            for (int i = 0; i < playerCount; i++)
            {
                int index = Random.Range(0, 3);
                int zoneIndex = Random.Range(0, playerZone.Length);

                if (playerZone[zoneIndex].transform.childCount == 0)
                {
                    //Vector3 position = tileTransform.position + Vector3.back;
                    var zoneObj = Instantiate(playerPrefab[index], playerZone[zoneIndex].transform.position, playerZone[zoneIndex].transform.rotation);
                    zoneObj.transform.parent = playerZone[zoneIndex].transform;
                    zoneObj.GetComponent<Weapon>().Setup(enemySpawner);
                }
                else
                {
                    Debug.Log("컨티뉴함");

                    break;
                }
                yield return new WaitForSeconds(0.6f);
            }
            yield return new WaitForSeconds(10.0f);

        }
    }
       
}
