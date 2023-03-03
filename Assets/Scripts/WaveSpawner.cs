using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject[] enemyPrefab;
    PlayerSpawner playerSpawner;
    //PoolManager poolManager;

    [SerializeField]
    private GameObject enemyHPSliderPrefab;     //적 체력 프리팹
    [SerializeField]
    private Transform canvasTransform;  //ui를 표현하는 canvas 오브젝트의 transform

    public int enemyCount = 20;
    public int waveCount = 1;
    public float waveEnd = 10;
    public int waveText = 1;
    [SerializeField]
    private PlayerGold playerGold;

    int index = 0;


    private List<Enemy> enemyList;
    public List<Enemy> EnemyList => enemyList;

    private void Awake()
    {
        //적 리스트 메모리 할당
        enemyList = new List<Enemy>();
    }

    void Start()
    {
        playerSpawner = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();
        //poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>(); ;
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        WaveEnemy();

        UIManager.Instance.waveCount.text = "Count : " + enemyList.Count;
        GameManager.Instance.enemyList = this.enemyList.Count;
    }

    public void WaveEnemy()
    {
        waveEnd -= Time.deltaTime;
        if (waveEnd <= 0)
        {
            waveText++;
            UIManager.Instance.wavetext.text = "WAVE : " + waveText;
            
            StartCoroutine(SpawnEnemy());
            StartCoroutine(playerSpawner.SpawnPlayer());
            waveEnd = 10;
        }
        UIManager.Instance.waveTime.text = "Time : " + (int)waveEnd;
    }

    IEnumerator SpawnEnemy()
    {
        Debug.Log("스폰 됐음");
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject clone = Instantiate(enemyPrefab[index], new Vector3(-8,6,0), Quaternion.identity);
            Enemy enemy = clone.GetComponent<Enemy>();
            enemyList.Add(enemy);
            SpawnEnemyHPSlider(clone);

            //GameObject obj = poolManager.Get(enemyPrefab[index].name);
            //Enemy enemy = obj.GetComponent<Enemy>();

            /*
            if (obj != null)
            {
                obj.SetActive(true);
                enemyList.Add(enemy);
                SpawnEnemyHPSlider(obj);
            }
            */

            yield return new WaitForSeconds(1.2f);
        }
        if (index != 3)
        {
            index++;
        }
        else
        {
            index = 0;
        }

    }

    public void Destroyenemy(Enemy enemy, int gold, GameObject sliderHP)
    {
        playerGold.CurrentGold += gold;
        UIManager.Instance.waveGold.text = "Gold : " + playerGold.CurrentGold;

        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
        //Destroy(sliderHP.gameObject);
    }

    public void SpawnEnemyHPSlider(GameObject enemy)
    {
        //적 체력을 나타내는 Slider UI 생성
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;

        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }

}
