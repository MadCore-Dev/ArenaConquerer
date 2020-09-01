using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class BaseAI : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;
    protected GameObject target;
    protected NavmeshTargetRandomiser navmeshTargetRandomiser;

    public Targeter targetType;
    [Header("Debug")]
    public bool debugDetails = false;
    public Color debugColor = Color.red;

    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navmeshTargetRandomiser = NavmeshTargetRandomiser.Instance;
    }

    protected virtual void Update() { }
    protected virtual void onTargetReached() { }
    protected virtual void RefreshTarget(Targeter targetType)
    {
        if (target == null)
        {
            onTargetReached();
            target = SetTarget(targetType);
        }
        if (target != null && Vector3.Distance(transform.position, navMeshAgent.destination) < 1f)
        {
            navmeshTargetRandomiser.UseUpTarget(target);
            onTargetReached();
            target = SetTarget(targetType);
        }
    }
    protected GameObject SetTarget(Targeter targetType, GameObject target = null)
    {
        switch (targetType)
        {
            case Targeter.Player:
                target = navmeshTargetRandomiser.GetPlayer();
                break;
            case Targeter.Random:
                target = navmeshTargetRandomiser.GetRandomTarget();
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
    }
}
public enum Targeter
{
    Player,
    Random,
    Manual
}
