using UnityEngine;
using DG.Tweening;

/// <summary>
/// Only added rough loop rotating logic
/// Will added better rotation logic in next step
/// </summary>
public class BashPiller : MonoBehaviour
{
    private Sequence pillerSequence;
    private void Start()
    {
        DOTween.Init();
        pillerSequence = DOTween.Sequence();

        LoopRotate();
    }

    private void LoopRotate()
    {
        transform.DORotate(new Vector3(0, 360, 0), 1f ,RotateMode.FastBeyond360).SetLoops(-1);    
    }
    
}
