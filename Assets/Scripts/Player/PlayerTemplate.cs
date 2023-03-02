using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerTemplate : ScriptableObject
{
    public GameObject playerPrefab;
    public Weapon[] weapon;

    [System.Serializable]
    public enum Weapon
    {
        sprite,
        damage,
        rate,
        range
    }
}
