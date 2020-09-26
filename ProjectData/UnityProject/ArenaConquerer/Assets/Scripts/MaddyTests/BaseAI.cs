using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class BaseAI : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;
    protected GameObject target;
    protected AIManager aiManager;

    public Targeter targetType;
    public float vision = 2.5f;
    [Header("Debug")]
    public bool debugDetails = false;
    public Color debugColor = Color.red;

    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        aiManager = AIManager.Instance;
    }

    protected virtual void Update() { }
    protected virtual void onTargetReached() { }
    protected virtual void RefreshTarget(Targeter targetType)
    {
        if (Vector3.Distance(transform.position, aiManager.GetPlayer().transform.position) < vision)
        {
            target = SetTarget(Targeter.Player);
        }
        else
        {
            if (target == null || target == aiManager.GetPlayer())
            {
                onTargetReached();
                target = SetTarget(targetType);
            }
            if (target != null && Vector3.Distance(transform.position, navMeshAgent.destination) < 1f)
            {
                if (target != aiManager.GetPlayer())
                {
                    aiManager.UseUpTarget(target);
                }
                onTargetReached();
                target = SetTarget(targetType);
            }
        }
    }
    protected GameObject SetTarget(Targeter targetType, GameObject target = null)
    {
        switch (targetType)
        {
            case Targeter.Player:
                target = aiManager.GetPlayer();
                break;
            case Targeter.Random:
                target = aiManager.GetRandomTarget();
                break;
            default:
                break;
        }
        navMeshAgent.SetDestination(target.transform.position);
        return target;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = debugColor;
        Handles.color = debugColor;
        if (debugDetails)
        {
            Handles.DrawSolidDisc(transform.position, transform.up, vision);
        }
    }
}
public enum Targeter
{
    Player,
    Random,
    Manual
}
