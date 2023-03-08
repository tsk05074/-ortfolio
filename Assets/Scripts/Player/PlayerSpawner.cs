using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform[] playerZone;
    public float playerCount = 2;
    [SerializeField]
    private WaveSpawner enemySpawner;

    [SerializeField]
    private PlayerTemplate[] playerTemplate;

    void Start()
    {
        enemySpawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
    }

    public IEnumerator SpawnPlayer()
    {
        if (GameManager.Instance.isGameStart)
        {
            for (int i = 0; i < playerCount; i++)
            {
                int index = Random.Range(0, 3);

                int zoneIndex = Random.Range(0, playerTemplate.Length);

                if (playerZone[zoneIndex].transform.childCount == 0)
                {
                    var zoneObj = Instantiate(playerTemplate[index].playerprefab, playerZone[zoneIndex].transform.position, playerZone[zoneIndex].transform.rotation);
                    zoneObj.transform.parent = playerZone[zoneIndex].transform;
                    zoneObj.GetComponent<Weapon>().Setup(enemySpawner);
                }
                else if (playerZone[zoneIndex].transform.childCount != 0)
                {
                    Debug.Log("다시");

                    while (true){
                        int zoneIndex2 = Random.Range(0, playerTemplate.Length);

                        if (playerZone[zoneIndex2].transform.childCount == 0)
                        {
                            Debug.Log("성공");
                            var zoneObj = Instantiate(playerTemplate[index].playerprefab, playerZone[zoneIndex2].transform.position, playerZone[zoneIndex2].transform.rotation);
                            zoneObj.transform.parent = playerZone[zoneIndex2].transform;
                            zoneObj.GetComponent<Weapon>().Setup(enemySpawner);
                            break;
                        }
                    }
                  
                }
                yield return new WaitForSeconds(0.6f);
            }
        }

    }

    
}
