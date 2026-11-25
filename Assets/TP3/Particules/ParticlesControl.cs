using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; 

public class ParticlesControl : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        
    }

    void OnEnable()
    {
        StartCoroutine(WaitForParticles());
    }

    IEnumerator WaitForParticles()
    {
        
        while (true)
        {
            
            yield return new WaitForSeconds(3f);

            
            ChangeParticlesRate();
        }
    }
    void ChangeParticlesRate()
    {
        
        var emission = _particleSystem.emission;
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(Random.Range(10f, 30f));
    }
}
