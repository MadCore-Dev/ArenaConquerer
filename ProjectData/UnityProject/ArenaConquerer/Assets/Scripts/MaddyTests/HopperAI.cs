using System.Collections.Generic;
using UnityEngine;

public class HopperAI : BaseAI
{
    [Min(0.1f)]
    public float hopDistance = 1f;

    protected override void Start()
    {
        base.Start();
        navMeshAgent.updatePosition = false;
        StartCoroutine(Hop(hopDistance));
    }

    protected override void Update()
    {
        RefreshTarget(targetType);
        transform.LookAt(navMeshAgent.nextPosition);
    }

    private IEnumerator<float> Hop(float delay)
    {
        float time = 0f;
        while (time < delay)
        {
            time += Time.deltaTime;
            yield return 0f;
        }
        transform.position = navMeshAgent.nextPosition;
        StartCoroutine(Hop(delay));
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (debugDetails)
        {
            if (navMeshAgent != null)
            {
                Gizmos.DrawSphere(navMeshAgent.nextPosition, 0.5f);
            }
        }
    }
}
