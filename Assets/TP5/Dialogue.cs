using UnityEngine;

public class Dialogue : MonoBehaviour
{

    [SerializeField] private AudioClip wowGreat;
    private AudioSource audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")&&other.gameObject.GetComponent<CharacterController>()!=null)
        {
            Debug.Log(other.gameObject.name);
            audioSource.PlayOneShot(wowGreat);
        }
    }
  
}
