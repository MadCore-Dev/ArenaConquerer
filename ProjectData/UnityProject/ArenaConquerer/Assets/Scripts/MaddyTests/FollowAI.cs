using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowAI : BaseAI
{
    protected override void Update()
    {
        RefreshTarget(targetType);
    }
}
