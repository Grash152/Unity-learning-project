using UnityEngine;

public class AnimationParticleControl : MonoBehaviour
{
    [SerializeField] private ParticleSystem myParticleSystem;
    public void ActivateParticles()
    {
            myParticleSystem.Play();
    }
    
    public void DeactivateParticles()
    {
            myParticleSystem.Stop();
    }
}
