using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;

    private Transform target;
    private float damage;

    public void Setup(Transform target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    void Update()
    {
        if (target != null) //target이 존재하면
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {

        if (!collision.CompareTag("Enemy1") && !collision.CompareTag("Enemy2") && !collision.CompareTag("Enemy3"))
        {
            return;
        }
        
        if (collision.transform != target) return; //현재 target인 적이 아닐 때

        collision.GetComponent<EnemyHP>().TakeDamage(damage);

        Destroy(this.gameObject);
    }
}
