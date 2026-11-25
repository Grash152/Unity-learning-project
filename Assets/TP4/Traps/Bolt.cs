using System.Collections;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    private bool stuck = false;
    private Rigidbody rb;
    private Collider coll;
    private SkinnedMeshRenderer sr;
    [SerializeField] private GameObject Body;
    [SerializeField] private AudioClip boltHit;
    AudioSource audioSource;
    private bool once = true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        sr = Body.GetComponent<SkinnedMeshRenderer>();
    }
 private void OnCollisionEnter(Collision other)
 {
     if (other.gameObject.tag != "Trap"&& !stuck && other.gameObject.layer != LayerMask.NameToLayer("Player"))
     {
         if (once)
         {
             audioSource.PlayOneShot(boltHit);
             once = false;
         }

         rb.linearVelocity = Vector3.zero;
         rb.angularVelocity = Vector3.zero;
         rb.isKinematic = true;
         if(coll != null)
             coll.enabled = false;
         if (other.gameObject.CompareTag("Player"))
         {
             HPSystem.SetCurrentHp(HPSystem.GetCurrentHp()-5);
             StartCoroutine(Hurt());
             Transform bone = other.collider.transform;
             transform.SetParent(bone);
             transform.localPosition = Vector3.zero;
             transform.position += transform.forward * 0.3f;
         }
         stuck = true;
     }
 }

 IEnumerator Hurt()
 {
     if(sr !=  null)
         sr.material.color = Color.red;
     yield return new WaitForSeconds(1f);
     if(sr !=  null)
         sr.material.color = Color.white;
 }
}
