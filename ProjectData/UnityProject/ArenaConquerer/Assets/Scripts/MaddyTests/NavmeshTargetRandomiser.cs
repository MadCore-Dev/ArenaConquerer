using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NavmeshTargetRandomiser : MonoBehaviour
{
    #region Singleton

    private static NavmeshTargetRandomiser _instance;

    public static NavmeshTargetRandomiser Instance { get { return _instance; } }


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

    [ContextMenu("SpawnTargets")]

    private void Update()
    {
        if (targetHolder.childCount < 5)
        {
            SpawnTargets();
        }
    }

    void SpawnTargets()
    {
        //foreach (Transform item in targetHolder)
        //{
        //    Destroy(item.gameObject);
        //}
        //targetData.Clear();
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
        if (targetData.Count > 0)
        { 
            targetDatas = targetData[Random.Range(0, targetData.Count)]; 
        }
        targetData.Remove(targetDatas);
        return targetDatas;
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
}
