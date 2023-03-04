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
    private GameObject enemyHPSliderPrefab;     //�� ü�� ������
    [SerializeField]
    private Transform canvasTransform;  //ui�� ǥ���ϴ� canvas ������Ʈ�� transform

    public int enemyCount = 20;
    public int waveCount = 1;
    public float waveEnd = 40;
    public int waveText = 1;
    [SerializeField]
    private PlayerGold playerGold;

    int index = 0;
    bool isSpawn = false;

    private List<Enemy> enemyList;
    public List<Enemy> EnemyList => enemyList;

    private void Awake()
    {
        //�� ����Ʈ �޸� �Ҵ�
        enemyList = new List<Enemy>();
    }

    void Start()
    {
        playerSpawner = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();
        //poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>(); ;
    }

    void Update()
    {
        WaveEnemy();
        //StartCoroutine(SpawnEnemy());

        UIManager.Instance.waveCount.text = "Count : " + enemyList.Count;
        GameManager.Instance.enemyList = this.enemyList.Count;
    }

    public void WaveEnemy()
    {
        if (GameManager.Instance.isGameStart)
        {
            //StartCoroutine(SpawnEnemy());
            waveEnd -= Time.deltaTime;
            if (waveEnd <= 0)
            {
                waveText++;
                UIManager.Instance.wavetext.text = "WAVE : " + waveText;

                //StartCoroutine(SpawnEnemy());
                StartCoroutine(playerSpawner.SpawnPlayer());
                waveEnd = 40;
            }
            UIManager.Instance.waveTime.text = "Time : " + (int)waveEnd;
        }        
    }

    public IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(0.1f);


        for (int i = 0; i < enemyCount; i++)
            {

                GameObject clone = Instantiate(enemyPrefab[index], new Vector3(-8, 6, 0), Quaternion.identity);
                Enemy enemy = clone.GetComponent<Enemy>();
                EnemyHP enemyhp = clone.GetComponent<EnemyHP>();
            if (GameManager.Instance.isEasy)
            {
                enemyhp.Setup(5);
            }
            else if (GameManager.Instance.isNormal)
            {
                Debug.Log("�븻 ����");
                enemyhp.Setup(10);
            }
            else if (GameManager.Instance.isHard)
            {
                enemyhp.Setup(15);
            }
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

                yield return new WaitForSeconds(1.0f);
            }
            if (index != 3)
            {
                index++;
                isSpawn = false;
            }
            else
            {
                index = 0;
                isSpawn = false;
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
        //�� ü���� ��Ÿ���� Slider UI ����
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;

        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }

}
