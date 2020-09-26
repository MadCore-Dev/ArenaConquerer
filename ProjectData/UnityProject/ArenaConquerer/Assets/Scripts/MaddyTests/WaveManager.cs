using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using CustomAttributes;

public class WaveManager : MonoBehaviour
{
    public float spwanRange = 10f;
    public Transform enemyHolder;
    [SerializeField, Readonly] private int enemiesPerWave = 0;
    [SerializeField, Readonly] private int currentWave = 1;
    [SerializeField, Readonly] private float remainingTimeOfWave = 0f;

    void Start()
    {
        enemiesPerWave = ArenaData.Instance.NumberOfEnemiesPerWave;
        StartCoroutine(InitateWave());
    }

    private IEnumerator<float> InitateWave()
    {
        SetupEnemies();
        remainingTimeOfWave = ArenaData.Instance.TimePerWaveInSeconds;
        while (remainingTimeOfWave > 0f)
        {
            remainingTimeOfWave += Time.deltaTime;
            yield return 0f;
        }
        if (currentWave < ArenaData.Instance.noOfWaves)
        {
            currentWave++;
            StartCoroutine(InitateWave());
        }
    }
    private void SetupEnemies()
    {
        float enemiesThisWave = enemiesPerWave * currentWave;
        for (int i = 0; i < enemiesThisWave; i++)
        {
            Vector2 randomPOsition = Random.insideUnitCircle * spwanRange;
            GameObject e = Instantiate(ArenaData.Instance.GetRandomEnemy(), transform.position + new Vector3(randomPOsition.x, 0, randomPOsition.y), Quaternion.identity, enemyHolder);
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.up, spwanRange);
    }
}