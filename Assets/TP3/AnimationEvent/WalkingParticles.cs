using UnityEngine;

public class WalkingParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftFootParticles;
    [SerializeField] private ParticleSystem rightFootParticles;
    public void ActivateParticlesLeftFoot()
    {
        leftFootParticles.Play();
    }
    
    public void DeactivateParticlesLeftFoot()
    {
       leftFootParticles.Stop();
    }
    public void ActivateParticlesRightFoot()
    {
        rightFootParticles.Play();
    }
    
    public void DeactivateParticlesRightFoot()
    {
       rightFootParticles.Stop();
    }
}
