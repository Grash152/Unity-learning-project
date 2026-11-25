using UnityEngine;

public class IgnoreParentCollision : MonoBehaviour
{
    void Start()
    {
        Collider parentCollider = GetComponent<Collider>();
        Collider childCollider = GetComponentInChildren<BoxCollider>();

        if (parentCollider != null && childCollider != null)
        {
            // Tells Unity's physics engine to completely ignore collisions between these two
            Physics.IgnoreCollision(parentCollider, childCollider, true);
        }
    }
}
