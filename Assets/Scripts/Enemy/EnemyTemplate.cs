using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class EnemyTemplate : ScriptableObject
{
    public new string name;

    public GameObject[] enemyprefab;
    public GameObject bossprefab;

    public Sprite[] img;
    public float maxHp;
    public float currentHP;
    public float speed;
}
