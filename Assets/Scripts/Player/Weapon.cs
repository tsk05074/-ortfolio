using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, TryAttackCannon, TryAttackSword }
public enum WeaponType { Cannon = 0, Sword,}

public class Weapon : MonoBehaviour
{
    [Header("Commons")]
    [SerializeField]
    private Transform spawnPoint;   //발사체 생성 위치
    [SerializeField]
    private WeaponType weaponType;  //무기 속성 설정

    [Header("Cannon")]
    [SerializeField]
    private GameObject projecttilePrefab;   //발사체 프리팹

    public float attackRate = 0.5f; //공격 속도
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
       //이전에 재생중이던 상태 종료
        StopCoroutine(weaponState.ToString());
        //상태 변경
        weaponState = newState;
        //새로운 상태 재생
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
            //attackRate 시간만큼 대기
            yield return new WaitForSeconds(attackRate);

            //공격
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
            //attackRate 시간만큼 대기
            yield return new WaitForSeconds(attackRate);

            //공격
            SpawnProjectTile();
            isAttack = true;
            animator.SetBool("isAttack", true);
        }
    }

    private Transform FindClosestAttackTarget()
    {
        //제일 가까이 있는 적 최초 거리 설정
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
        //target이 있는지 검사
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
