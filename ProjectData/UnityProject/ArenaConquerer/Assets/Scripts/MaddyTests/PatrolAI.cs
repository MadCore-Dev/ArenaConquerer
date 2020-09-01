using UnityEditor;
using UnityEngine;

public class PatrolAI : BaseAI
{
    public Color patrolPointsColor= Color.blue;
    [Header("Properties")]
    public Transform patrolPointsHolder;
    public int currentTarget = 0;
    public int direction = 1;
    public float vision = 2.5f;

    protected override void Start()
    {
        base.Start();
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
        if (Vector3.Distance(transform.position, navmeshTargetRandomiser.GetPlayer().transform.position) < vision)
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
        if (target != navmeshTargetRandomiser.GetPlayer() && Vector3.Distance(transform.position, target.transform.position) < 1f)
        {
            onTargetReached();
        }
    }
    protected override void Update()
    {
        RefreshTarget(Targeter.Manual);
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (debugDetails)
        {
            Handles.DrawWireDisc(transform.position, transform.up, vision);
            Gizmos.color = patrolPointsColor;
            for (int i = 0; i < patrolPointsHolder.childCount; i++)
            {
                if (i < patrolPointsHolder.childCount - 1)
                {
                    Gizmos.DrawLine(patrolPointsHolder.GetChild(i).transform.position, patrolPointsHolder.GetChild(i+1).transform.position);
                }
                Gizmos.DrawSphere(patrolPointsHolder.GetChild(i).transform.position, 0.5f);
            }
        }
    }
}
