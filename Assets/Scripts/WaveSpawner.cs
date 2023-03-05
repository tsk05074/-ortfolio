using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    PlayerSpawner playerSpawner;
    [SerializeField]
    private EnemyTemplate enemyTemplate;
    [SerializeField]
    private EnemyTemplate enemyBossTemplate;
    //PoolManager poolManager;

    [SerializeField]
    private GameObject enemyHPSliderPrefab;     //적 체력 프리팹
    [SerializeField]
    private Transform canvasTransform;  //ui를 표현하는 canvas 오브젝트의 transform

    public int enemyCount = 20;
    public int waveCount = 1;
    public float waveEnd = 30;
    public int waveText = 1;
    public float waveCheck = 1f;

    [SerializeField]
    private int enemyHP = 5;
    [SerializeField]
    private PlayerGold playerGold;

    int index = 0;
    bool isSpawn = false;

    private List<Enemy> enemyList;
    public List<Enemy> EnemyList => enemyList;

    private void Awake()
    {
        //적 리스트 메모리 할당
        enemyList = new List<Enemy>();

        enemyTemplate.maxHp = 5;
        enemyBossTemplate.maxHp = 100;
    }

    void Start()
    {
        playerSpawner = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();
        //poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>(); ;
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
            waveEnd = 40;
            StartCoroutine(playerSpawner.SpawnPlayer());
            waveCheck++;
        }
        UIManager.Instance.waveTime.text = "Time : " + (int)waveEnd;
    }

    public IEnumerator SpawnEnemy()
    {
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                //GameObject clone = Instantiate(enemyPrefab[index], new Vector3(-8, 6, 0), Quaternion.identity);
                GameObject clone = Instantiate(enemyTemplate.enemyprefab[index], new Vector3(-8, 6, 0), Quaternion.identity);
                Enemy enemy = clone.GetComponent<Enemy>();
                EnemyHP enemyhp = clone.GetComponent<EnemyHP>();
                if (GameManager.Instance.isEasy)
                {
                    enemyhp.Setup(enemyTemplate.maxHp);

                }
                else if (GameManager.Instance.isNormal)
                {
                    enemyhp.Setup(enemyTemplate.maxHp * 2);
                }
                else if (GameManager.Instance.isHard)
                {
                    enemyhp.Setup(enemyTemplate.maxHp * 3);
                }
                enemyList.Add(enemy);
                SpawnEnemyHPSlider(clone);

                yield return new WaitForSeconds(1.0f);
            }
            
            enemyTemplate.maxHp += 2;

            if (index < 2)
            {
                index++;
                isSpawn = false;
            }
            else if (index >= 2)
            {
                index = 0;
                isSpawn = false;
            }
            yield return new WaitForSeconds(10.0f);

            if (waveCheck%5 ==0)
            {
                for (int i = 0; i < 1; i++)
                {
                    GameObject boss = Instantiate(enemyBossTemplate.bossprefab, new Vector3(-8, 6, 0), Quaternion.identity);
                    Enemy bossenemy = boss.GetComponent<Enemy>();
                    EnemyHP bossenemyhp = boss.GetComponent<EnemyHP>();

                    if (GameManager.Instance.isEasy)
                    {
                        bossenemyhp.Setup(enemyBossTemplate.maxHp);

                    }
                    else if (GameManager.Instance.isNormal)
                    {
                        bossenemyhp.Setup(enemyBossTemplate.maxHp * 2);
                    }
                    else if (GameManager.Instance.isHard)
                    {
                        bossenemyhp.Setup(enemyBossTemplate.maxHp * 3);
                    }

                    enemyList.Add(bossenemy);
                    SpawnEnemyHPSlider(boss);
                }
            }
            
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
