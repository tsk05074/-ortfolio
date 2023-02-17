using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;
    WaveSpawner wavespawner;

    public Text wavetext;
    public Text waveTime;
    public Text waveCount;

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
        wavetext.text = "WAVE" + wavespawner.waveText;
        waveTime.text = "Time : " + wavespawner.waveEnd;
        waveCount.text = "Count : " + wavespawner.waveCount;
    }

    void Update()
    {

    }
}
