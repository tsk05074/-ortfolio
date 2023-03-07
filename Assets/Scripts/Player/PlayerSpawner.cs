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

    [SerializeField]
    private PlayerTemplate[] playerTemplate;

    public List<int> tileZone = new List<int>();
    private int randomunber;
    DragandDrop dad;

    void Start()
    {
        tileZone = new List<int>(new int[playerZone.Length]);

        for (int i = 0; i < playerZone.Length; i++)
        {
            randomunber = Random.Range(1, playerZone.Length + 1);

            while (tileZone.Contains(randomunber))
            {
                randomunber = Random.Range(1, playerZone.Length + 1);
            }

            tileZone[i] = randomunber;
            //break;
        }

        //PlayerSpawner playerspawner = gameObject.GetComponent<PlayerSpawner>();
        enemySpawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        //StartCoroutine("SpawnPlayer");
        dad = GetComponent<DragandDrop>();
    }

    public IEnumerator SpawnPlayer()
    {
        //yield return new WaitForSeconds(0.1f);
        if (GameManager.Instance.isGameStart)
        {
            for (int i = 0; i < playerCount; i++)
            {
                int index = Random.Range(0, 3);
                int zoneIndex = Random.Range(0, tileZone.Count);

                if (playerZone[zoneIndex].transform.childCount == 0)
                {
                    //var zoneObj = Instantiate(playerPrefab[index], playerZone[zoneIndex].transform.position, playerZone[zoneIndex].transform.rotation);
                    //var zoneObj = Instantiate(playerTemplate[index].playerprefab, playerZone[zoneIndex].transform.position, playerZone[zoneIndex].transform.rotation);
                    //zoneObj.transform.parent = playerZone[zoneIndex].transform;
                    //zoneObj.GetComponent<Weapon>().Setup(enemySpawner);

                    var zoneObj = Instantiate(playerTemplate[index].playerprefab, playerZone[zoneIndex].transform.position, playerZone[zoneIndex].transform.rotation);
                    zoneObj.transform.parent = playerZone[zoneIndex].transform;
                    zoneObj.GetComponent<Weapon>().Setup(enemySpawner);
                    tileZone.RemoveAt(zoneIndex);
                }
                else
                {
                    int zoneIndex2 = Random.Range(0, tileZone.Count);

                    var zoneObj = Instantiate(playerTemplate[index].playerprefab, playerZone[zoneIndex2].transform.position, playerZone[zoneIndex2].transform.rotation);
                    zoneObj.transform.parent = playerZone[zoneIndex2].transform;
                    zoneObj.GetComponent<Weapon>().Setup(enemySpawner);
                    tileZone.RemoveAt(zoneIndex);

                }


                yield return new WaitForSeconds(0.6f);
            }
        }
      
    }

        //int zoneIndex = Random.Range(min, max);

        //for (int i=0; i< max;)
        //{
        //    if (tileZone.Contains(zoneIndex))
        //    {
        //        zoneIndex = Random.Range(min, max);
        //    }
        //    else
        //    {
        //        Debug.Log("중복값 발생");

        //        tileZone.Add(zoneIndex);
        //        i++;
        //    }
        //}

    
}
