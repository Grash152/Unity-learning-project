using UnityEngine;

public class Wind : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                Vector3 direction =transform.TransformDirection(Vector3.forward).normalized;
                controller.Move(direction * Time.deltaTime * 10);
            }
        }
    }
}
