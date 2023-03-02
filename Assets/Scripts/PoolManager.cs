using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pooltem
{
    public GameObject prefab;
    public int amount;
}
public class PoolManager : MonoBehaviour
{
    public static PoolManager singleton;
    public List<Pooltem> items;
    public List<GameObject> pooledItems;

    WaveSpawner waveSpawner;


    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        pooledItems = new List<GameObject>();
        foreach (Pooltem item in items)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);

                //waveSpawner.EnemyList.Add(enemy);
                //waveSpawner.SpawnEnemyHPSlider(obj);
            }
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        for (int i = 0; i < pooledItems.Count; i++)
        {
            if (!pooledItems[i].activeInHierarchy)
            {
                select = pooledItems[i];

              

                return pooledItems[i];
            }
        }
        //Enemy enemy = pooledItems[index].GetComponent<Enemy>();

        //waveSpawner.EnemyList.Add(enemy);
        //waveSpawner.SpawnEnemyHPSlider(pooledItems[index]);

        //if (!select)
        //{
        //    select = Instantiate(pooledItems[index], transform);
        //    Enemy enemy = select.GetComponent<Enemy>();

        //    waveSpawner.EnemyList.Add(enemy);
        //    waveSpawner.SpawnEnemyHPSlider(select);
        //    //pooledItems[index].Add(select);
        //}

        return null;
    }

    

    /*
    //프리팹들을 보관할 변수
    public GameObject[] prefabs;
    WaveSpawner waveSpawner;

    //풀 담당을 하는 리스트들
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    private void Start()
    {
        waveSpawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject enemy in pools[index])
        {
            if (!enemy.activeSelf)
            {
                select = enemy;
                enemy.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            Enemy enemy = select.GetComponent<Enemy>();

            waveSpawner.EnemyList.Add(enemy);
            waveSpawner.SpawnEnemyHPSlider(select);
            pools[index].Add(select);
        }

        return select;
    }
    */


}
