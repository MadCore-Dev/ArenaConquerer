using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAI : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;
    protected GameObject target;
    protected NavmeshTargetRandomiser navmeshTargetRandomiser;

    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navmeshTargetRandomiser = NavmeshTargetRandomiser.Instance;
    }

    protected virtual void Update() { }
    protected virtual void onTargetReached() { }
    protected void RefreshTarget()
    {
        SetTarget();
        if (target != null && Vector3.Distance(transform.position, target.transform.position) < 1f)
        {
            navmeshTargetRandomiser.UseUpTarget(target);
            SetTarget();
        }
    }
    private void SetTarget()
    {
        if (target == null)
        {
            target = navmeshTargetRandomiser.GetRandomTarget();
            navMeshAgent.SetDestination(target.transform.position);
            onTargetReached();
        }
    }
}
