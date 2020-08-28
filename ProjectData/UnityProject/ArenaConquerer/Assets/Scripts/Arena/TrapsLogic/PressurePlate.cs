using UnityEngine;
using DG.Tweening;

public class PressurePlate : Damage
{
    [SerializeField] private bool autoPlay = false;

    [SerializeField] private ParticleSystem smoke;
    private string objectTag = null;
    private const float damage = 5f;

    private void Start()
    {
        PlayPS();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == objectTag)
        {
            PlayPS();
        }

        if (collision.transform.tag == "Player")
        {
            DamageToPlayer(damage, collision.transform.GetComponent<Health>());
        }
    }

    public void OnParticleSystemStopped()
    {
        DOVirtual.DelayedCall(1f, PlayPS);
    }

    public void PlayPS()
    {
        smoke.Play();

        DOVirtual.DelayedCall(2f, () =>
        {
            smoke.Stop();
            if (autoPlay)
            {
                var main = smoke.main;
                main.stopAction = ParticleSystemStopAction.Callback;
            }
        });
    }
}
