using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public float timeScale;
    public int enemyList;
    [SerializeField]
    private int gameEndEnemy = 70;

    private void Awake()
    {
        Time.timeScale = 1.0f;

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

    }

    void Update()
    {
        GameEnd();
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
