using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject[] enemyPrefab;
    PlayerSpawner playerSpawner;

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
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        WaveEnemy();

        UIManager.Instance.waveCount.text = "Count : " + enemyList.Count;

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
        int index = Random.Range(0, 3);
        for (int i=0;i<enemyCount;i++)
        {
            GameObject clone = Instantiate(enemyPrefab[index]);
            Enemy enemy = clone.GetComponent<Enemy>();

            //Instantiate(enemyPrefab[index], enemyPrefab[index].transform.position, transform.rotation);
            
            enemyList.Add(enemy);

            SpawnEnemyHPSlider(clone);
            yield return new WaitForSeconds(0.6f);
        }
    }

    public void Destroyenemy(Enemy enemy, int gold)
    {
        Debug.Log("죽어야함");
        playerGold.CurrentGold += gold;
        UIManager.Instance.waveGold.text = "Gold : " + playerGold.CurrentGold;

        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    void SpawnEnemyHPSlider(GameObject enemy)
    {
        //적 체력을 나타내는 Slider UI 생성
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;

        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }

}
