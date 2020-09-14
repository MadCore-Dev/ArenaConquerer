using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    #region Singleton

    private static AIManager _instance;

    public static AIManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public GameObject targetPrefab;
    public Vector2 targetspawnRange;
    public int targetCounts;

    public List<GameObject> targetData = new List<GameObject>();
    public Transform targetHolder;
    public GameObject player;

    [Header("Patroling")]
    public Transform petrolPathsHolder;
    public bool debugDetails = true;
    public Color patrolPointsColor = Color.blue;

   [ContextMenu("SpawnTargets")]

    private void Update()
    {
        if (targetHolder.childCount < 5)
        {
            SpwanTargets();
        }
    }

    void SpwanTargets()
    {
        for (int i = 0; i < targetCounts; i++)
        {
            GameObject temp = Instantiate(targetPrefab, new Vector3(Random.Range(-targetspawnRange.x, targetspawnRange.x), 1, Random.Range(-targetspawnRange.y, targetspawnRange.y)), Quaternion.identity);
            temp.transform.SetParent(targetHolder);
            targetData.Add(temp);
        }
    }

    public GameObject GetRandomTarget()
    {
        GameObject targetDatas = null;
        if (targetData.Count < 5)
        {
            SpwanTargets();
        }
        if (targetData.Count > 0)
        { 
            targetDatas = targetData[Random.Range(0, targetData.Count)]; 
        }
        targetData.Remove(targetDatas);
        return targetDatas;
    }

    public GameObject GetPlayer()
    {
        if (player != null)
        {
            return player;
        }
        else
        {
            return GetRandomTarget();
        }
    }

    public void UseUpTarget(GameObject target)
    {
        foreach (Transform item in targetHolder)
        {
            if (target == item.gameObject)
            {
                Destroy(item.gameObject);
            }
        }
    }

    public Transform GetRandomPatrolPath()
    {
        return petrolPathsHolder.GetChild(Random.Range(0, petrolPathsHolder.childCount));
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(player.transform.position, Vector3.one);
        Gizmos.color = patrolPointsColor;
        if (debugDetails)
        {
            Gizmos.color = patrolPointsColor;
            foreach (Transform path in petrolPathsHolder)
            {
                for (int i = 0; i < path.childCount; i++)
                {
                    if (i < path.childCount - 1)
                    {
                        Gizmos.DrawLine(path.GetChild(i).transform.position, path.GetChild(i + 1).transform.position);
                    }
                    Gizmos.DrawSphere(path.GetChild(i).transform.position, 0.5f);
                }
            }
        }
    }
}
