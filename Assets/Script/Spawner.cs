using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float timer;
    [SerializeField] private float spawnFrequency;
    public List<Monster> monsterList = new List<Monster>();
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnFrequency)
        {
            SpawnMonster(monsterList[Random.Range(0, monsterList.Count)]);
        }
    }

    public void SpawnMonster(Monster _monster)
    {
        Monster _monsterInstance = Instantiate(_monster);
        _monsterInstance.transform.position = transform.position;
        timer = 0;
    }

}
