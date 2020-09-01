using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class HopperAI : BaseAI
{
    [Min(0.1f)]
    public float hopDistance = 1f;
    IEnumerator hopper;
    protected override void Start()
    {
        base.Start();
        navMeshAgent.updatePosition = false;
    }

    protected override void onTargetReached()
    {
        if(hopper == null)
        {
            hopper = Hop(hopDistance);
            StartCoroutine(hopper);
        }
        else
        {
            StopCoroutine(hopper);
        }
    }

    protected override void Update()
    {
        RefreshTarget();
        transform.LookAt(navMeshAgent.nextPosition);
    }

    IEnumerator<float> Hop(float delay)
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

    private void OnDrawGizmos()
    {
        if(navMeshAgent != null)
        {
            Gizmos.DrawSphere(navMeshAgent.nextPosition, 0.5f);
        }
    }
}
