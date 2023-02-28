using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    private Transform target;
    private int wavepointIndex = 0;
    WaveSpawner wavespawner;

    [SerializeField]
    private int gold = 10; //Àû »ç¸Á ½Ã È¹µæ °¡´ÉÇÑ °ñµå

    void Start()
    {
        target = WayPoints.points[0];
        wavespawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
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
    }

    public void OnDie()
    {
        wavespawner.Destroyenemy(this, gold);
    }
}
