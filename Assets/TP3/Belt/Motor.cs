using UnityEngine;

public class Motor : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] private Vector3 rotationAxis = Vector3.right;
    private Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; 
    }

    void FixedUpdate()
    {
        float angleThisFrame = speed * Time.fixedDeltaTime;
        Quaternion rotationThisFrame = Quaternion.AngleAxis(angleThisFrame, rotationAxis);
        
        rb.MoveRotation(rb.rotation * rotationThisFrame);
    }
        
    
}
