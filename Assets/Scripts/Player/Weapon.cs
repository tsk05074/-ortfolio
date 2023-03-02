using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, AttackToTarget}

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private PlayerTemplate playerTemplate;
    [SerializeField]
    private GameObject projecttilePrefab;   //발사체 프리팹
    [SerializeField]
    private Transform spawnPoint;   //발사체 생성 위치

    public float attackRate = 0.5f; //공격 속도
    public float attackRange = 2.0f;

    public static int attackDamage = 1;
    public int maxAttackDamage = 9;

    private WeaponState weaponState = WeaponState.SearchTarget; 

    private Transform attackTarget = null;

    WaveSpawner enemySpawner;
    public int level = 0;
    //public float Damage => attackDamage;
    //public float Rate => attackRate;
    //public float Range => attackRange;
    //public float Damage => playerTemplate.weapon[level].damage;
    //public float Rate => playerTemplate.weapon[level].rate;
    //public float Range => playerTemplate.weapon[level].range;

    
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
            float closestDistSqr = Mathf.Infinity;

            for (int i = 0; i<enemySpawner.EnemyList.Count; ++i){

                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                //현재 검사중인 적과의 거리가 공격범위 내에 있고, 현재까지 검사한 적보다 거리가 가까우면
                if (distance <= attackRange && distance <= closestDistSqr)
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
            //target이 있는지 검사
            if (attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            //target이 공격 범위 안에 있는지 검사(공격 범위를 벗어나면 새로운 적 탐색)
            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if (distance > attackRange)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            //attackRate 시간만큼 대기
            yield return new WaitForSeconds(attackRate);

            //공격
            SpawnProjectTile();
        }
    }

    void SpawnProjectTile()
    {
        GameObject clone = Instantiate(projecttilePrefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<Projectile>().Setup(attackTarget, attackDamage);
    }
}
