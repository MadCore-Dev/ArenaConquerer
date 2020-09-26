using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New ArenaData Object", menuName = "Scriptable Objects/Arena Data", order = 1)]
public class ArenaData : ScriptableObject
{
    [Tooltip("No of waves of the fight")] public int noOfWaves = 0;
    [Tooltip("In minutes")] public float timePerWave = 0;
    [Tooltip("Min No. of enemies")] public int min;
    [Tooltip("Max No. of enemies")] public int max;
    public List<EnemyWaveData> enemyTypesData = new List<EnemyWaveData>();

    #region Singleton

    public static ArenaData Instance;
    public ArenaData()
    {
        if (Instance == null || Instance != this)
        {
            Instance = this;
        }
    }

    #endregion

    #region UI TO CONFIG

    public void UpdateWaves(int waves)
    {
        noOfWaves = waves;
    }
    public void UpdateMinEnemies(int value)
    {
        min = value;
    }
    public void UpdateMaxEnemies(int value)
    {
        max = value;
    }
    public void UpdateWaveTime(int time)
    {
        timePerWave = time;
    }
    public void SetEnemyTypeData(int enemyType, float weight)
    {
        EnemyWaveData enemyData = enemyTypesData.Where(e => e.typeOfEnemy == (EnemyType)enemyType).FirstOrDefault();
        if (enemyData != null)
        {
            enemyData.weight = weight * 100f;
            if (weight > 0)
            {
                enemyData.enabled = true;
            }
            else
            {
                enemyData.enabled = false;
            }
        }
    } 

    #endregion

    #region Utility

    const float SECONDS_PER_MIN = 2f;
    public int NumberOfEnemiesPerWave => Random.Range(min, max);
    public float TimePerWaveInSeconds => timePerWave * SECONDS_PER_MIN;

    [ContextMenu("Reset Data")]
    public void Reset()
    {
        noOfWaves = 0;
        timePerWave = 0;
        foreach (EnemyWaveData enemy in enemyTypesData)
        {
            enemy.enabled = false;
            enemy.weight = 0f;
        }
    }
    public GameObject GetRandomEnemy()
    {
        float sum_of_weight = 0;
        for (int i = 0; i < enemyTypesData.Count; i++)
        {
            sum_of_weight += enemyTypesData[i].weight;
        }
        float rnd = Random.Range(0, sum_of_weight);
        for (int i = 0; i < enemyTypesData.Count; i++)
        {
            if (rnd < enemyTypesData[i].weight)
            {
                return enemyTypesData[i].enemy;
            }
            rnd -= enemyTypesData[i].weight;
        }
        return null;
    } 

    #endregion
}

[System.Serializable]
public class EnemyWaveData
{
    public EnemyType typeOfEnemy;
    public bool enabled;
    public GameObject enemy;
    public float weight;
}

public enum EnemyType
{
    Follow,
    Hopper,
    Patrol
}