using UnityEngine;

public class AnimatonEventParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;

    public void PlayParticles()
    {
        _particles.Play();
    }

    public void StopParticles()
    {
        _particles.Stop();
    }
}
