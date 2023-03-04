using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    private UIManager uiMG;
    public WaveSpawner waveSpawnMG;
    private PlayerSpawner playerSpawnMG;
    public EnemyHP enemyHPMG;

    public int enemyList;
    [SerializeField]
    private int gameEndEnemy = 70;

    public float timeScale;

    public bool isGameStart = false;
    public bool isEasy;
    public bool isNormal;
    public bool isHard;

    private void Awake()
    {
        Time.timeScale = timeScale;

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

    public static GameManager Instance
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

    void Start()
    {
        playerSpawnMG = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();
        uiMG = GetComponent<UIManager>();
        waveSpawnMG = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        //enemyHPMG = GameObject.Find("EnemyHP").GetComponent<EnemyHP>();
    }

    void Update()
    {
        GameEnd();
    }

    public IEnumerator GameStart()
    {
        isGameStart = true;
       
        Time.timeScale = 1.0f;


        StartCoroutine(playerSpawnMG.SpawnPlayer());
        StartCoroutine(waveSpawnMG.SpawnEnemy());

        yield return null;

    }

    void GameEnd()
    {
        if (enemyList > 70)
        {
            UIManager.Instance.gameOverUI.gameObject.SetActive(true);
            Time.timeScale = timeScale;
        }
    }

    public void GameReStart(string str)
    {
        SceneManager.LoadScene(str);
    }
}
