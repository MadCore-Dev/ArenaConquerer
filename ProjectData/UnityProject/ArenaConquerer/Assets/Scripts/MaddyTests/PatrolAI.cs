using UnityEditor;
using UnityEngine;

public class PatrolAI : BaseAI
{
    private Transform patrolPointsHolder;
    private int currentTarget = 0;
    [Header("Properties")]
    public int direction = 1;

    protected override void Start()
    {
        base.Start();
        patrolPointsHolder = aiManager.GetRandomPatrolPath();
        target = patrolPointsHolder.GetChild(currentTarget).gameObject;
        SetTarget(Targeter.Manual, target);
    }

    protected override void onTargetReached()
    {
        currentTarget += direction;
        if (currentTarget == patrolPointsHolder.childCount - 1 || currentTarget == 0)
        {
            direction *= -1;
        }
        target = patrolPointsHolder.GetChild(currentTarget).gameObject;
        target = SetTarget(Targeter.Manual, target);
    }
    protected override void RefreshTarget(Targeter targetType)
    {
        if (Vector3.Distance(transform.position, aiManager.GetPlayer().transform.position) < vision)
        {
            target = SetTarget(Targeter.Player);
        }
        else
        {
            if (target != patrolPointsHolder.GetChild(currentTarget).gameObject)
            {
                target = SetTarget(Targeter.Manual, patrolPointsHolder.GetChild(currentTarget).gameObject);
            }
        }
        if (target != aiManager.GetPlayer() && Vector3.Distance(transform.position, target.transform.position) < 1f)
        {
            onTargetReached();
        }
    }
    protected override void Update()
    {
        RefreshTarget(Targeter.Manual);
    }
}
