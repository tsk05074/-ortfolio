using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, TryAttackCannon, TryAttackSword }
public enum WeaponType { Cannon = 0, Sword,}

public class Weapon : MonoBehaviour
{
    [Header("Commons")]
    [SerializeField]
    private Transform spawnPoint;   //�߻�ü ���� ��ġ
    [SerializeField]
    private WeaponType weaponType;  //���� �Ӽ� ����

    [Header("Cannon")]
    [SerializeField]
    private GameObject projecttilePrefab;   //�߻�ü ������

    public float attackRate = 0.5f; //���� �ӵ�
    public float attackRange = 2.0f;
    public static int attackDamage = 1;
    public int maxAttackDamage = 9;

    public PlayerTemplate playerTemplate;

    bool isAttack;

    private WeaponState weaponState = WeaponState.SearchTarget; 

    private Transform attackTarget = null;
    Animator animator;

    WaveSpawner enemySpawner;
    public int level = 0;
    
    void Start()
    {
        animator = GetComponent<Animator>();
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

            attackTarget = FindClosestAttackTarget();

            if (attackTarget != null)
            {
                if (weaponType == WeaponType.Cannon)
                {
                    ChangeState(WeaponState.TryAttackCannon);
                }
                else if (weaponType == WeaponType.Sword)
                {
                    ChangeState(WeaponState.TryAttackSword);
                }
                //ChangeState(WeaponState.AttackToTarget);
            }

            yield return null;
        }
    }

    //private IEnumerator AttackToTarget()
    private IEnumerator TryAttackCannon()
    {
        while (true)
        {
            if (IsPossibleToAttackTarget() == false)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            //attackRate �ð���ŭ ���
            yield return new WaitForSeconds(attackRate);

            //����
            SpawnProjectTile();
            isAttack = true;
            animator.SetBool("isAttack", true);
        }
    }

    private IEnumerator TryAttackSword()
    {
        while (true)
        {
            if (IsPossibleToAttackTarget() == false)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            //attackRate �ð���ŭ ���
            yield return new WaitForSeconds(attackRate);

            //����
            SpawnProjectTile();
            isAttack = true;
            animator.SetBool("isAttack", true);
        }
    }

    private Transform FindClosestAttackTarget()
    {
        //���� ������ �ִ� �� ���� �Ÿ� ����
        float closestDistSqr = Mathf.Infinity;

        for (int i=0; i<enemySpawner.EnemyList.Count; i++)
        {
            float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);

            if (distance <= playerTemplate.attackRange && distance <= closestDistSqr)
            {
                closestDistSqr = distance;
                attackTarget = enemySpawner.EnemyList[i].transform;
            }
        }
        return attackTarget;
    }

    private bool IsPossibleToAttackTarget()
    {
        //target�� �ִ��� �˻�
        if (attackTarget == null)
        {
            return false;
        }

        float distance = Vector3.Distance(attackTarget.position, transform.position);
        if (distance > playerTemplate.attackRange)
        {
            animator.SetBool("isAttack", false);

            attackTarget = null;
            return false;
        }

        return true;
    }

    void SpawnProjectTile()
    {
        GameObject clone = Instantiate(projecttilePrefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<Projectile>().Setup(attackTarget, playerTemplate.attackDamage);

    }
}
