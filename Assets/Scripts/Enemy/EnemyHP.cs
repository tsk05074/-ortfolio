using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    public float maxHP;    //최대 체력
    public float currentHP;    //현재 체력
    private bool isDie = false;
    private Enemy enemy;
    private SpriteRenderer spriteRenderer;

    //public float MaxHP => maxHP;
    //public float CurrentHP => currentHP;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(float enemyHP)
    {
        this.maxHP = enemyHP;
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        if (isDie == true) return;

        currentHP -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        if (currentHP <= 0)
        {
            isDie = true;
            enemy.OnDie();
        }
    }

    IEnumerable HitAlphaAnimation()
    {
        Color color = spriteRenderer.color;

        color.a = 0.4f;
        spriteRenderer.color = color;

        yield return new WaitForSeconds(0.05f);

        color.a = 1.0f;

        spriteRenderer.color = color;
    }
 
}
