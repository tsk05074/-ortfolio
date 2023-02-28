using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;
    WaveSpawner wavespawner;
    PlayerGold playerGold;
    

    public TextMeshProUGUI wavetext;
    public TextMeshProUGUI waveTime;
    public TextMeshProUGUI waveCount;
    public TextMeshProUGUI waveGold;

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

    public static UIManager Instance
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
        wavespawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        playerGold = GameObject.Find("PlayerSpawner").GetComponent<PlayerGold>();
        wavetext.text = "WAVE : " + wavespawner.waveText;
        waveTime.text = "Time : " + wavespawner.waveEnd;
        waveCount.text = "Count : " + wavespawner.EnemyList.Count;
        waveGold.text = "Gold : " + playerGold.CurrentGold;
    }

    void Update()
    {
    }
}
