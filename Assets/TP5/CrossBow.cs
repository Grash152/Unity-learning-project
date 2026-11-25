using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
public class CrossBow : MonoBehaviour
{
    [SerializeField] private GameObject Crossbow;
    [SerializeField] private GameObject Bolt;
    [SerializeField] private AudioClip bowSound;
    private ObjectPool<GameObject> _pool;
    RaycastHit hitWindow;
    RaycastHit hitPlayer;
    private int rayNumber = 25;
    private Rigidbody rb;
    private bool cd = true;
    private GameObject projectile;
    private Vector3 targetPoint;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        _pool = new ObjectPool<GameObject>(
            createFunc: CreateItem,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,
            defaultCapacity: 1,
            maxSize: 10
        );
    }
    
    
    void Update()
    {
        float startAngle = 60f;
        float entreAngle = 3;
        
        
        for (int i = 0; i < rayNumber; i++)
        {
            float currentAngle = startAngle + (entreAngle * i);
            Quaternion targetRotation = Quaternion.Euler(0f, currentAngle, 0f);
            Vector3 directionToTarget = targetRotation * Vector3.forward;
            Physics.Raycast(transform.position, directionToTarget, out hitPlayer,
                Mathf.Infinity);
            Debug.DrawRay(transform.position, directionToTarget*20f, Color.red);
            if (hitPlayer.collider != null && hitPlayer.collider.CompareTag("Player"))
            {
                Vector3 PlayerVelocity = Vector3.zero;
                CharacterController cc = hitPlayer.collider.gameObject.GetComponent<CharacterController>();
                if (cc != null)
                {
                    PlayerVelocity = cc.velocity;
                }
                Vector3 PredictedPosition = hitPlayer.transform.position + (PlayerVelocity * 0.3f);
                targetPoint = Crossbow.transform.position - PredictedPosition;
                targetPoint.y = 0;
                Crossbow.transform.rotation = Quaternion.LookRotation(targetPoint);
            
                if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hitWindow, Mathf.Infinity, LayerMask.GetMask("Ignore Raycast")))
                {
                    if (cd)
                    {
                        StartCoroutine(Shoot());
                    }

                }
                
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 20f, Color.yellow);
        }
    }

    IEnumerator Shoot()
    {
        cd = false;
        yield return new WaitForSeconds(0.5f);
        projectile = _pool.Get();
        
        if (projectile != null)
        {
            projectile.transform.SetParent(null);
            rb = projectile.GetComponent<Rigidbody>();
            projectile.transform.rotation = Quaternion.LookRotation(targetPoint);
            if (rb != null)
            {
                rb.AddForce(-projectile.transform.forward * 50, ForceMode.Impulse);
                audioSource.PlayOneShot(bowSound);
            }
        }
        StartCoroutine(ReturnAfter(projectile));
        yield return new WaitForSeconds(3f);
        cd = true;

    }
    private GameObject CreateItem()
    {
        GameObject pooledObject = Instantiate(Bolt);
        pooledObject.name = Bolt.name;
        pooledObject.SetActive(false);
        return pooledObject;
    }
    
    private void OnGet(GameObject pooledObject)
    {
        pooledObject.transform.SetParent(transform);
        pooledObject.transform.localPosition = Vector3.zero;
        pooledObject.SetActive(true);
    }

   
    private void OnRelease(GameObject pooledObject)
    {
   
        pooledObject.SetActive(false);
    }
    
    private void OnDestroyItem(GameObject pooledObject)
    {
        Destroy(pooledObject);
    }

    IEnumerator ReturnAfter(GameObject pooledObject)
    {
        yield return new WaitForSeconds(20f);
        pooledObject.SetActive(false);
    }
}
