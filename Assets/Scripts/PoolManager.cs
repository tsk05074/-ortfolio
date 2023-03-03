using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[System.Serializable]
//public class Pooltem
//{
//    public GameObject prefab;
//    public int amount;
//}
//[System.Serializable]
//public class SliderItem
//{
//    public GameObject prefab;
//    public int amount;
//}

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance = null;

    //public List<Pooltem> items;
    //public List<SliderItem> sliderItems;

    //public List<GameObject> pooledItems;
    //public List<GameObject> SliderHPs;

    //WaveSpawner waveSpawner;

    //public Transform canvasTransform;  //ui를 표현하는 canvas 오브젝트의 transform

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static PoolManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    //private void Start()
    //{
    //    pooledItems = new List<GameObject>();
    //    SliderHPs = new List<GameObject>();

    //    foreach (Pooltem item in items)
    //    {
    //        for (int i = 0; i < item.amount; i++)
    //        {
    //            GameObject obj = Instantiate(item.prefab);
    //            obj.SetActive(false);
    //            pooledItems.Add(obj);

    //            //waveSpawner.EnemyList.Add(enemy);
    //            //waveSpawner.SpawnEnemyHPSlider(obj);
    //        }
    //    }

    //    foreach (SliderItem item in sliderItems)
    //    {
    //        for (int i = item.amount; i <= item.amount; i++)
    //        {
    //            GameObject obj = Instantiate(item.prefab);
    //            //obj.transform.SetParent(canvasTransform);
    //            //obj.transform.localScale = Vector3.one;
    //            obj.SetActive(false);
    //            SliderHPs.Add(obj);

    //            //waveSpawner.EnemyList.Add(enemy);
    //            //waveSpawner.SpawnEnemyHPSlider(obj);
    //        }
    //    }
    //}

    //public GameObject Get()
    //{
    //    int index = 0;
    //    for (int i = 0; i < pooledItems.Count; i++)
    //    {
    //        index++;
    //        if (!pooledItems[index].activeInHierarchy)
    //        {
    //            return pooledItems[index];
    //        }

    //        if (index == pooledItems.Count)
    //        {
    //            index = 0;
    //        }
    //    }
    //    return null;
    //}

    //public GameObject SliderGet()
    //{
    //    for (int i = 0; i < SliderHPs.Count; i++)
    //    {
    //        if (!SliderHPs[i].activeInHierarchy)
    //        {
    //            return SliderHPs[i];
    //        }
    //    }
    //    return null;
    //}

    [SerializeField]
    private GameObject[] objectPrefabs;

    private List<GameObject> pooledObjects = new List<GameObject>();

    public GameObject Get(string type)
    {
        foreach (GameObject go in pooledObjects)
        {
            if (go.name == type && !go.activeInHierarchy)
            {
                go.SetActive(true);
                return go;
            } 
        }

        for (int i=0; i< objectPrefabs.Length; i++)
        {
            if (objectPrefabs[i].name == type)
            {
                GameObject newObject = Instantiate(objectPrefabs[i]);
                newObject.name = type;
                pooledObjects.Add(newObject);
                return newObject;
            }
        }

        return null;
    }

    public void ReleaseObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

}
