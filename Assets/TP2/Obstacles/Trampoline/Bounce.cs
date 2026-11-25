using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using System.Collections;


public class Bounce : MonoBehaviour
{
    [SerializeField] private float launchForce;
    private bool cd;
    private PlayerInteractions controller;
    private void OnTriggerEnter(Collider other)
    {
        if (cd)
            return;
        Rigidbody rb = other.attachedRigidbody;

        if (rb != null)
        {
            cd=true;
            controller = rb.GetComponentInParent<PlayerInteractions>();
            if (controller != null) 
            {
                controller.GoRagdoll();
            }

            Rigidbody raghip = controller.GetRagdollHips();
            raghip.AddForce(new Vector3(Random.Range(0f,0.2f),1,Random.Range(0f,0.2f)) * launchForce, ForceMode.Impulse);
            
        }
        Invoke(nameof(Reset),1f);
    }
    
    private void Reset()
    {
        cd = false;
        
    }
}
