using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    private UIManager uiMG;
    private WaveSpawner waveSpawnMG;
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

    private InterstitialAd interstitial;

    private void Awake()
    {
        //Time.timeScale = timeScale;

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
        RequestIntersititial();

        SoundeManager.Instance.PlayBGM("TitleBGM");

        playerSpawnMG = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();
        uiMG = GetComponent<UIManager>();
        waveSpawnMG = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        //enemyHPMG = GameObject.Find("EnemyHP").GetComponent<EnemyHP>();
    }

    void Update()
    {
        GameEnd();
        GameSuccess();
    }

    public IEnumerator GameStart()
    {
        isGameStart = true;
       
        //Time.timeScale = 1.0f;

        yield return new WaitForSeconds(0.1f);
        StartCoroutine(playerSpawnMG.SpawnPlayer());
        StartCoroutine(waveSpawnMG.SpawnEnemy());

        //yield return null;

    }

    private void RequestIntersititial()
    {
#if UNITY_ANDROID
        string adUnitld = "ca-app-pub-4659297465066412/9030835687";
#elif UNITY_IPHONE
        string adUnitid = "unexpected_platform";
#else
        string adUnitid = "unexpected_platform";
#endif

        this.interstitial = new InterstitialAd(adUnitld);

        this.interstitial.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();

        this.interstitial.LoadAd(request);
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //SoundeManager.Instance.PlaySFX("ClickSFX");
        SceneManager.LoadScene("Title");
    }

    void GameEnd()
    {
        if (enemyList > 70)
        {
            RetryGamePlay();
            UIManager.Instance.gameOverUI.gameObject.SetActive(true);
            Time.timeScale = timeScale;
        }
    }

    public void GameSuccess()
    {
        if (waveSpawnMG.waveText > 20 && enemyList > 70)
        {
            RetryGamePlay();
            UIManager.Instance.gameClearUI.gameObject.SetActive(true);
            Time.timeScale = timeScale;
        }
    }

    private void GameOver()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    public void RetryGamePlay()
    {
        GameOver();
    }
}
