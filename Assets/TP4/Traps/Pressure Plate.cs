using UnityEngine;

public class PressurePlate : MonoBehaviour
{
   [SerializeField] private GameObject CrossBow;
   [SerializeField] private GameObject Bolt;
   private bool once = false;
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag("Player")&&!once)
      {
         Vector3 PlayerVelocity = Vector3.zero;
         CharacterController cc = other.gameObject.GetComponent<CharacterController>();
         if (cc != null)
         {
            PlayerVelocity = cc.velocity;
         }
         Vector3 PredictedPosition = other.transform.position + (PlayerVelocity * 0.3f);
         Vector3 targetPoint = CrossBow.transform.position - PredictedPosition;
         targetPoint.y = 0;
         CrossBow.transform.rotation = Quaternion.LookRotation(targetPoint);
         Rigidbody rb = Bolt.GetComponent<Rigidbody>();
         if (rb != null)
         {
            Bolt.transform.SetParent(null);
            rb.AddForce(-Bolt.transform.forward * 50,ForceMode.Impulse);
         }
         once = true;
      }
   }
}
