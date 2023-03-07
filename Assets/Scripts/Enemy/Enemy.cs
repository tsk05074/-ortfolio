using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    private Transform target;
    private int wavepointIndex = 0;
    WaveSpawner wavespawner;
    public GameObject sliderHP;
    [SerializeField]
    EnemyTemplate enemytemplate;

    private SpriteRenderer sRenderer;

    [SerializeField]
    private int gold = 10; //�� ��� �� ȹ�� ������ ���

    void Start()
    {
        enemytemplate.speed = speed;
        target = WayPoints.points[0];
        wavespawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.flipX = false;
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();

        }
    }

    void GetNextWayPoint()
    {
        if (wavepointIndex >= WayPoints.points.Length -1)
        {
            wavepointIndex = 0;
            target = WayPoints.points[wavepointIndex];
        }
        else
        {
            wavepointIndex++;
            target = WayPoints.points[wavepointIndex];
        }

        if (wavepointIndex >= 2)
        {
            sRenderer.flipX = true;
        }
        else if(wavepointIndex < 2)
        {
            sRenderer.flipX = false;
        }


    }

    public void OnDie()
    {
        wavespawner.Destroyenemy(this, gold, sliderHP);
    }

}
