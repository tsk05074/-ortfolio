using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerTemplate : ScriptableObject
{
    public new string name;

    public GameObject playerprefab;

    public Sprite img;
    public float attackRate;
    public float attackRange;
    public int attackDamage;

}
