using UnityEngine;

public class Tile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
