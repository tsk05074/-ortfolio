using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;
    WaveSpawner wavespawner;
    PlayerGold playerGold;
    //Weapon weapon;
    public Weapon currentPlayer;
    private PlayerTemplate playerTemplate;  //플레이어 정보
    public EnemyHP enemyhp;

    public TextMeshProUGUI wavetext;
    public TextMeshProUGUI waveTime;
    public TextMeshProUGUI waveCount;
    public TextMeshProUGUI waveGold;

    [SerializeField]
    private Image imagePlayer;
    [SerializeField]
    private TextMeshProUGUI textDamage;
    [SerializeField]
    private TextMeshProUGUI textRate;
    [SerializeField]
    private TextMeshProUGUI textRange;
    [SerializeField]
    private PlayerAttackRange playerAttackRange;

    [SerializeField]
    private TextMeshProUGUI textHP;
    [SerializeField]
    private TextMeshProUGUI textSpeed;

    [SerializeField]
    private GameObject palyerInfo;

    public bool IsplayerInfo;

    [Header("PlayerCanvasInfo")]
    [SerializeField]
    private GameObject information;
    [SerializeField]
    private GameObject playerInfo;
    [SerializeField]
    private GameObject playerInfoObj;
    [SerializeField]
    private GameObject upgrade;
    [SerializeField]
    private GameObject combination;

    [Header("EnemyCanvasInfo")]
    [SerializeField]
    private Image imageEnemy;
    [SerializeField]
    private GameObject enemyInfo;
    [SerializeField]
    private GameObject enemyInfoObj;
    [SerializeField]
    private GameObject enemyHP;
    [SerializeField]
    private GameObject enemySpeed;


    [Header("UpgradeButton")]
    [SerializeField]
    public GameObject upgradeButton1;
    [SerializeField]
    public GameObject upgradeButton2;
    [SerializeField]
    public GameObject upgradeButton3;
    [SerializeField]
    public GameObject upgradeButton4;

    [Header("GameOverUI")]
    public GameObject gameOverUI;
    public GameObject gameClearUI;

    [Header("GameLevelUI")]
    public GameObject gameLevelUI;

    [Header("GameCombiUI")]
    public Button xConbiButton;
    public RectTransform combiTransform;

    PlayerTemplate player3;
    PlayerTemplate player2;
    PlayerTemplate player1;

    [SerializeField]
    EnemyTemplate enemytemplate;

    [Header("PlayerSprite")]
    Image thisimg;
    public Sprite player3_img;
    public Sprite player2_img;
    public Sprite player1_img;
    public Sprite player4_img;
    public Sprite player5_img;
    public Sprite player6_img;
    public Sprite player7_img;
    public Sprite player8_img;
    public Sprite player9_img;



    private void Awake()
    {
        information.SetActive(false);
        playerInfo.SetActive(false);
        upgrade.SetActive(false);
        combination.SetActive(false);

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
        enemyhp = GetComponent<EnemyHP>();
        wavetext.text = "WAVE : " + wavespawner.waveText;
        waveTime.text = "Time : " + wavespawner.waveEnd;
        waveCount.text = "Count : " + wavespawner.EnemyList.Count;
        waveGold.text = "Gold : " + playerGold.CurrentGold;

        thisimg = GetComponent<Image>();
    }

    public void OnPnanel(Transform playerWeapon, bool isplayer)
    {
        //출력해야하는 타워 정보를 받아와서 저장
        currentPlayer = playerWeapon.GetComponent<Weapon>();

        if (IsplayerInfo && isplayer)
        {
            playerInfo.SetActive(true);
            palyerInfo.gameObject.SetActive(true);
            enemyInfo.gameObject.SetActive(false);

            if (playerWeapon.CompareTag("Player3"))
            {
                imagePlayer.sprite = player3_img;
            }
            else if (playerWeapon.CompareTag("Player2"))
            {
                imagePlayer.sprite = player2_img;

            }
            else if (playerWeapon.CompareTag("Player1"))
            {
                imagePlayer.sprite = player1_img;
            }
            else if (playerWeapon.CompareTag("Player4"))
            {
                imagePlayer.sprite = player4_img;
            }
            else if (playerWeapon.CompareTag("Player5"))
            {
                imagePlayer.sprite = player5_img;
            }
            else if (playerWeapon.CompareTag("Player6"))
            {
                imagePlayer.sprite = player6_img;
            }
            else if (playerWeapon.CompareTag("Player7"))
            {
                imagePlayer.sprite = player7_img;
            }
            else if (playerWeapon.CompareTag("Player8"))
            {
                imagePlayer.sprite = player8_img;
            }
            else if (playerWeapon.CompareTag("Player9"))
            {
                imagePlayer.sprite = player9_img;
            }


            //타워 정보 갱신
            UpdatePlayerData(imagePlayer, currentPlayer);
        }
   
        //타워 오브젝트 주변에 표시되는 타워 공격범위 Sprite On
        playerAttackRange.OnAttackRange(currentPlayer.transform.position, currentPlayer.attackRange);
        playerAttackRange.AttackRangePosition(currentPlayer.transform.position);
    }

    public void OffPanel()
    {
        playerAttackRange.OffAttackRange();
    }

    public void CombiFadeIn()
    {
        combination.SetActive(true);
        combiTransform.transform.localPosition = new Vector3(0f, -1000f, 0f);
        combiTransform.DOAnchorPos(new Vector2(0f, 0f), 0.5f, false).SetEase(Ease.OutElastic);
    }

    public void CombiFadeOut()
    {
        combiTransform.transform.localPosition = new Vector3(0f, -0f, 0f);
        combiTransform.DOAnchorPos(new Vector2(0f, -1100f), 0.5f, false).SetEase(Ease.InOutQuint);
    }

    private void UpdatePlayerData(Image img, Weapon weapon)
    {
        imagePlayer = img;
        textDamage.text = "Damage :" + currentPlayer.attackDamage;
        textRange.text = "Range : " + currentPlayer.attackRange;
        textRate.text = "Rate : " + currentPlayer.attackRate;
    }


    public void CanVasUI(string obj)
    {
        information.SetActive(false);
        playerInfo.SetActive(false);
        playerInfoObj.SetActive(false);
        upgrade.SetActive(false);
        combination.SetActive(false);
        IsplayerInfo = false;

        if (obj.ToString() == "information")
        {
            information.SetActive(true);
        }
        else if (obj.ToString() == "playerInfo")
        {
            playerInfoObj.SetActive(true);
            IsplayerInfo = true;
        }
        else if (obj.ToString() == "upgrade")
        {
            upgrade.SetActive(true);
        }
        else if (obj.ToString() == "combination")
        {
            combination.SetActive(true);
        }
        else if(obj.ToString() == "X")
        {
            combination.SetActive(false);
        }

        SoundeManager.Instance.PlaySFX("ClickSfx");

    }

    public void UpgradeWeapon(string obj)
    {

        if (obj.ToString() == "이지" && playerGold.CurrentGold >= 2)
        {
            SoundeManager.Instance.PlaySFX("ClickSfx");

            int gold = 20;
            //Weapon.attackDamage++;
           
            if (currentPlayer.CompareTag("Player1") || currentPlayer.CompareTag("Player2") || currentPlayer.CompareTag("Player3"))
            {
                currentPlayer.attackDamage++;
                textDamage.text = "Damage :" + currentPlayer.attackDamage;
                playerGold.CurrentGold -= gold;
                waveGold.text = "Gold : " + playerGold.CurrentGold;

                if (currentPlayer.attackDamage > currentPlayer.maxAttackDamage)
                {
                    currentPlayer.attackDamage = currentPlayer.maxAttackDamage;
                }
            }
        }
        if (obj.ToString() == "노멀" && playerGold.CurrentGold >= 4)
        {
            SoundeManager.Instance.PlaySFX("ClickSfx");

            int gold = 40;

            if (currentPlayer.CompareTag("Player4") || currentPlayer.CompareTag("Player5") || currentPlayer.CompareTag("Player6"))
            {
                currentPlayer.attackDamage++;
                textDamage.text = "Damage :" + currentPlayer.attackDamage;
                playerGold.CurrentGold -= gold;
                waveGold.text = "Gold : " + playerGold.CurrentGold;

                if (currentPlayer.attackDamage > currentPlayer.maxAttackDamage)
                {
                    currentPlayer.attackDamage = currentPlayer.maxAttackDamage;

                }
            }
        }
        if (obj.ToString() == "하드" && playerGold.CurrentGold >= 10)
        {
            SoundeManager.Instance.PlaySFX("ClickSfx");

            int gold = 60;
            if (currentPlayer.CompareTag("Player7") || currentPlayer.CompareTag("Player8") || currentPlayer.CompareTag("Player9"))
            {
                currentPlayer.attackDamage++;
                textDamage.text = "Damage :" + currentPlayer.attackDamage;
                playerGold.CurrentGold -= gold;
                waveGold.text = "Gold : " + playerGold.CurrentGold;

                if (currentPlayer.attackDamage > currentPlayer.maxAttackDamage)
                {
                    currentPlayer.attackDamage = currentPlayer.maxAttackDamage;

                }
            }
        }

    }

    public void LevelButton(GameObject obj)
    {
        SoundeManager.Instance.PlaySFX("ClickSfx");

        StartCoroutine(GameManager.Instance.GameStart());

        if (obj.name == "Easy")
        {
            GameManager.Instance.isEasy = true;
            GameManager.Instance.isNormal = false;
            GameManager.Instance.isHard = false;

        }
        else if (obj.name == "Normal")
        {
            GameManager.Instance.isEasy = false;
            GameManager.Instance.isNormal = true;
            GameManager.Instance.isHard = false;

        }
        else if(obj.name == "Hard")
        {
            GameManager.Instance.isEasy = false;
            GameManager.Instance.isNormal = false;
            GameManager.Instance.isHard = true;
        }

        gameLevelUI.SetActive(false);

    }

}
