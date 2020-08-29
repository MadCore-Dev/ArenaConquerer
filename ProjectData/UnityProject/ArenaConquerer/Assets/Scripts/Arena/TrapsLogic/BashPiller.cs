﻿using UnityEngine;
using DG.Tweening;

/// <summary>
/// Only added rough loop rotating logic
/// Will added better rotation logic in next step
/// </summary>
public class BashPiller : Damage
{
    private const float damage = 5f;
    private void Start()
    {
        DOTween.Init();

        LoopRotate();
    }

    private void LoopRotate()
    {
        transform.DORotate(new Vector3(0, 360, 0), 1f ,RotateMode.FastBeyond360).SetLoops(-1);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            SingleDamageInstance(damage, other.transform.GetComponent<Health>());
        }
    }

}
