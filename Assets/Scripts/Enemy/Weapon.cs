using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, AttackToTarget}

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private PlayerTemplate playerTemplate;
    [SerializeField]
    private GameObject projecttilePrefab;   //�߻�ü ������
    [SerializeField]
    private Transform spawnPoint;   //�߻�ü ���� ��ġ
    //[SerializeField]
    //private float attackRate = 0.5f; //���� �ӵ�
    //[SerializeField]
    //private float attackRange = 2.0f;
    //public int attackDamage = 1;
    //public int maxAttackDamage = 9;
    private WeaponState weaponState = WeaponState.SearchTarget; 

    private Transform attackTarget = null;

    WaveSpawner enemySpawner;
    int level = 0;
    //public float Damage => attackDamage;
    //public float Rate => attackRate;
    //public float Range => attackRange;
    public float Damage => playerTemplate.weapon[level].damage;
    public float Rate => playerTemplate.weapon[level].rate;
    public float Range => playerTemplate.weapon[level].range;


    void Start()
    {
        enemySpawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
    }

    public void Setup(WaveSpawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;
        ChangeState(WeaponState.SearchTarget);
    }

    public void ChangeState(WeaponState newState)
    {
       //������ ������̴� ���� ����
        StopCoroutine(weaponState.ToString());
        //���� ����
        weaponState = newState;
        //���ο� ���� ���
        StartCoroutine(weaponState.ToString());
    }

    void Update()
    {
        /*
        if (attackTarget != null)
        {
            RotateToTarget();
        }
        */
    }

    private IEnumerator SearchTarget()
    {
        while (true)
        {
            float closestDistSqr = Mathf.Infinity;

            for (int i = 0; i<enemySpawner.EnemyList.Count; ++i){

                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                //���� �˻����� ������ �Ÿ��� ���ݹ��� ���� �ְ�, ������� �˻��� ������ �Ÿ��� ������
                if (distance <= playerTemplate.weapon[level].damage && distance <= closestDistSqr)
                {
                    closestDistSqr = distance;
                    attackTarget = enemySpawner.EnemyList[i].transform;
                }
            }

            if (attackTarget != null)
            {
                ChangeState(WeaponState.AttackToTarget);
            }

            yield return null;
        }
    }

    private IEnumerator AttackToTarget()
    {
        while (true)
        {
            //target�� �ִ��� �˻�
            if (attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            //target�� ���� ���� �ȿ� �ִ��� �˻�(���� ������ ����� ���ο� �� Ž��)
            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if (distance > playerTemplate.weapon[level].range)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            //attackRate �ð���ŭ ���
            yield return new WaitForSeconds(playerTemplate.weapon[level].rate);

            //����
            SpawnProjectTile();
        }
    }

    void SpawnProjectTile()
    {
        GameObject clone = Instantiate(projecttilePrefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<Projectile>().Setup(attackTarget, playerTemplate.weapon[level].damage);
    }

    /*
    public void UpgradeWeapon(string obj)
    {
        if (obj.ToString() == "����")
        {
            attackDamage++;
            if (attackDamage > maxAttackDamage)
            {
                UIManager.Instance.upgradeButton1.SetActive(false);
            }
        }
    }
    */
}
