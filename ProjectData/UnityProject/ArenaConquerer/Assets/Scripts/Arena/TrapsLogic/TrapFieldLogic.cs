using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class TrapFieldLogic : Damage
{
    [SerializeField] private float targetPosition = 0;
    [SerializeField] private float delayBeforeHide = 0f;
    [SerializeField] private bool alwaysAnimating = false;

    [SerializeField] private List<Transform> spikes = new List<Transform>();
    private string tagName = null;
    private const float damage = 5f;

    Sequence spikeAnimate;

    private void Start()
    {
        DOTween.Init();

        for (int i = 0; i < spikes.Count; i++)
        {
            spikeAnimate = DOTween.Sequence();
            AnimateSpikeLoopWithDelay(targetPosition, spikes[i].localPosition.y, spikes[i]);
        }
        if (alwaysAnimating)
        {
            for (int i = 0; i < spikes.Count; i++)
            {
                AnimateSpikeLoopWithDelay(targetPosition, spikes[i].position.y, spikes[i]);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!alwaysAnimating)
        {
            if (other.gameObject.tag == tagName) // tagName will be player Tag
            {
                for (int i = 0; i < spikes.Count; i++)
                {
                    AnimateSpike(targetPosition, spikes[i].position.y, spikes[i]);
                }
            }
        }


        if (other.gameObject.tag == "Player")
        {
            //Here set some boolean on player enter for continue dmg (In Future)
            //So that we can stack effects on player
            SingleDamageInstance(damage, other.transform.GetComponent<Health>());
        }
    }

    /// <summary>
    /// Will Animate once only
    /// </summary>
    /// <param name="end_pos"></param>
    /// <param name="start_pos"></param>
    private void AnimateSpike(float end_pos, float start_pos, Transform spike)
    {
        spikeAnimate.Append(spike.DOLocalMoveY(end_pos, .5f).
                     SetEase(Ease.InOutSine).
                     SetDelay(delayBeforeHide)).
                     Append(spike.DOLocalMoveY(start_pos, .5f).
                     SetEase(Ease.InOutSine));
    }

    /// <summary>
    /// Will animate in loop with delay
    /// </summary>
    /// <param name="end_pos"></param>
    /// <param name="start_pos"></param>
    private void AnimateSpikeLoopWithDelay(float end_pos, float start_pos, Transform spike)
    {
        spikeAnimate.Append(spike.DOLocalMoveY(end_pos, .5f).
                     SetEase(Ease.InOutSine).
                     SetDelay(delayBeforeHide)).
                     Append(spike.DOLocalMoveY(start_pos, .5f).
                     SetEase(Ease.InOutSine).
                     SetDelay(delayBeforeHide)).
                     SetLoops(-1);
    }
}
