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
    //Weapon weapon;
    public Weapon currentPlayer;

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
    private GameObject palyerInfo;

    public bool IsplayerInfo;

    [Header("CanvasInfo")]
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

    [Header("UpgradeButton")]
    [SerializeField]
    public GameObject upgradeButton1;
    [SerializeField]
    public GameObject upgradeButton2;
    [SerializeField]
    public GameObject upgradeButton3;
    [SerializeField]
    public GameObject upgradeButton4;


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
        wavetext.text = "WAVE : " + wavespawner.waveText;
        waveTime.text = "Time : " + wavespawner.waveEnd;
        waveCount.text = "Count : " + wavespawner.EnemyList.Count;
        waveGold.text = "Gold : " + playerGold.CurrentGold;
    }

    public void OnPnanel(Transform playerWeapon)
    {
       
        //출력해야하는 타워 정보를 받아와서 저장
        currentPlayer = playerWeapon.GetComponent<Weapon>();

        if (IsplayerInfo)
        {
            playerInfo.SetActive(true);

            palyerInfo.gameObject.SetActive(true);

            //타워 정보 갱신
            UpdatePlayerData();
        }
      

        //타워 오브젝트 주변에 표시되는 타워 공격범위 Sprite On
        playerAttackRange.OnAttackRange(currentPlayer.transform.position, currentPlayer.Range);
        playerAttackRange.AttackRangePosition(currentPlayer.transform.position);
    }

    public void OffPanel()
    {
        playerAttackRange.OffAttackRange();
    }

    private void UpdatePlayerData()
    {
        textDamage.text = "Damage :" + currentPlayer.Damage;
        textRange.text = "Range : " + currentPlayer.Range;
        textRate.text = "Rate : " + currentPlayer.Rate;
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
    }

    /*
    public void UpgradeWeapon(string obj)
    {
        if (obj.ToString() == "흔함")
        {

            currentPlayer.attackDamage++;
            if (currentPlayer.attackDamage > currentPlayer.maxAttackDamage)
            {
                UIManager.Instance.upgradeButton1.SetActive(false);
            }
        }
    }
    */

}
