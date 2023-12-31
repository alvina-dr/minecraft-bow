using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("WAVE SYSTEM")]
    private int waveNumber = 0;
    [SerializeField] private float waveDifficultyMultiplier;
    [SerializeField] private int zombieNumber;
    [SerializeField] private float spawnFrequency;
    public List<Monster> monsterList = new List<Monster>();
    public List<Transform> spawnerPoints = new List<Transform>();

    [Header("UPGRADE")]
    [SerializeField] private ItemData arrowData;

    [Header("PAUSE")]
    private float pauseTimer;
    [SerializeField] private float pauseDuration;
    private bool wavePause = true;

    private void Update()
    {
        if (FindObjectsOfType<Monster>().Length == 0 && !wavePause)
        {
            WaitNextWave();
        }

        if (wavePause)
        {
            pauseTimer += Time.deltaTime;
            GPCtrl.Instance.UICtrl.chronoWavePause.SetText(Mathf.RoundToInt(pauseDuration - pauseTimer).ToString());
        }
        if (pauseTimer >= pauseDuration)
        {
            NextWave();
        }
    }

    public void WaitNextWave()
    {
        wavePause = true;
        zombieNumber = Mathf.RoundToInt(zombieNumber * waveDifficultyMultiplier);
    }

    public void NextWave()
    {
        GPCtrl.Instance.player.inventory.AddItem(arrowData, waveNumber * 15);
        pauseTimer = 0;
        waveNumber++;
        for (int i = 0; i < waveNumber * zombieNumber; i++)
        {
            SpawnMonster(monsterList[Random.Range(0, monsterList.Count)]);
        }
        wavePause = false;
        GPCtrl.Instance.UICtrl.chronoWavePause.SetText("");
    }

    public void SpawnMonster(Monster _monster)
    {
        Monster _monsterInstance = Instantiate(_monster);
        _monsterInstance.transform.position = spawnerPoints[Random.Range(0, spawnerPoints.Count)].position;
    }

}
