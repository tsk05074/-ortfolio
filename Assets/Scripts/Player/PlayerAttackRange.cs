using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRange : MonoBehaviour
{
    void Start()
    {
        OffAttackRange();
    }

    public void OnAttackRange(Vector3 posiiton, float range)
    {
        
        gameObject.SetActive(true);

        //공격 범위 크기
        float diameter = range * 2.0f;
        transform.localScale = Vector3.one * diameter;
        //공격 범위 위치
        transform.position = posiiton;
    }

    public void AttackRangePosition(Vector3 posiiton)
    {
        transform.position = posiiton;

    }

    private void Update()
    {
        transform.position = UIManager.Instance.currentPlayer.transform.position;
        //AttackRangePosition(UIManager.Instance.currentPlayer.transform.position);
    }

    public void OffAttackRange()
    {
        gameObject.SetActive(false);
    }
 
}
