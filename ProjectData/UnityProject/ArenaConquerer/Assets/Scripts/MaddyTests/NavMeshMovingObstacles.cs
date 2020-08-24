using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshMovingObstacles : MonoBehaviour
{
    public List<GameObject> obstacles;

    private void Update()
    {
        foreach (var item in obstacles)
        {
            Vector3 position = item.transform.position;
            position.y = Mathf.PingPong(Time.time, 5f) - 2.5f;
            item.transform.position = position;
        }
    }
}
