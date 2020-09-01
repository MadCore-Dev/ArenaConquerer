using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NavMeshAgents : BaseAI
{
    protected override void Update()
    {
        RefreshTarget(targetType);
    }
}
