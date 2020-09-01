using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgents : BaseAI
{
    protected override void Update()
    {
        RefreshTarget();
    }
}
