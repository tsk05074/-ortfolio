using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    PlayerSpawner playerSpawner;

    public int enemyCount = 20;
    public int waveCount = 1;
    public float waveEnd = 10;
    public int waveText = 1;

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
            UIManager.Instance.waveCount.text = "Count : " + i;

            GameObject clone = Instantiate(enemyPrefab[index]);
            Enemy enemy = clone.GetComponent<Enemy>();

            //Instantiate(enemyPrefab[index], enemyPrefab[index].transform.position, transform.rotation);
            
            enemyList.Add(enemy);
            yield return new WaitForSeconds(0.6f);
        }
    }

    public void Destroyenemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

}
