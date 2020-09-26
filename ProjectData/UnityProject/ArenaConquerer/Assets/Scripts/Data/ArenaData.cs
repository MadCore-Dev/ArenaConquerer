using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New ArenaData Object", menuName = "Scriptable Objects/Arena Data", order = 1)]
public class ArenaData : ScriptableObject
{
    public int noOfWaves = 0;
    [Tooltip("In minutes")] public float timePerWave = 0;
    public List<EnemyWaveData> enemyTypes = new List<EnemyWaveData>();

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

    public void UpdateWaves(int waves)
    {
        noOfWaves = waves;
    }
    public void UpdateWaveTime(int time)
    {
        timePerWave = time;
    }
    public void SetEnemyTypeData(int enemyType, float weight)
    {
        EnemyWaveData enemyData = enemyTypes.Where(e => e.typeOfEnemy == (EnemyType)enemyType).FirstOrDefault();
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

    [ContextMenu("Reset Data")]
    public void Reset()
    {
        noOfWaves = 0;
        timePerWave = 0;
        foreach (EnemyWaveData enemy in enemyTypes)
        {
            enemy.enabled = false;
            enemy.weight = 0f;
        }
    }
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