using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgents : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    GameObject target;
    NavmeshTargetRandomiser navmeshTargetRandomiser;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navmeshTargetRandomiser = NavmeshTargetRandomiser.Instance;
    }

    private void Update()
    {
        if (target == null)
        {
            target = navmeshTargetRandomiser.GetRandomTarget();
            navMeshAgent.SetDestination(target.transform.position);
        }
        if (target != null && Vector3.Distance(transform.position, target.transform.position) < 1f)
        {
            navmeshTargetRandomiser.UseUpTarget(target);
            target = navmeshTargetRandomiser.GetRandomTarget();
            navMeshAgent.SetDestination(target.transform.position);
        }
    }

}
