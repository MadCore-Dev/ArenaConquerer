using UnityEngine;
using DG.Tweening;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private bool autoPlay = false;

    private ParticleSystem smoke;
    private string objectTag = null;

    private void Start()
    {
        smoke = GetComponent<ParticleSystem>();
        PlayPS();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == objectTag)
        {
            PlayPS();
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
