using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class MonsterData : ScriptableObject
{
    public float attackRange;
    public float moveSpeed;
    public float maxHealth;
    public float damage;
    public float attackReload;
    public int killPoints;
    public List<ItemData> dropList = new List<ItemData>();
}
