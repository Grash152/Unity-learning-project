using UnityEngine;
using UnityEngine.Pool;

public class WoodPool : MonoBehaviour
{
        [SerializeField] private GameObject Wood;
        [SerializeField] private float spawnCooldown = 1f;
        private float _spawnTime;
      
        private ObjectPool<GameObject> _pool;
        
    
        private void Awake()
        {
           
            _pool = new ObjectPool<GameObject>(
                createFunc: CreateItem,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: OnDestroyItem,
                collectionCheck: true,   
                defaultCapacity: 10,
                maxSize: 20
            );
        }

       
        private void Update()
        {
            _spawnTime+=Time.deltaTime;
            
            if (_spawnTime >= spawnCooldown)
            {
                SpawnWood();
                _spawnTime = 0;
            
            }
        
        }

        private void SpawnWood()
        {
        
            GameObject wood = _pool.Get();
            wood.transform.position = transform.position;
            StartCoroutine(ReturnAfter(wood, 10f));
        }
        // Creates a new pooled GameObject the first time (and whenever the pool needs more).
        private GameObject CreateItem()
        {
            GameObject pooledObject = Instantiate(Wood.gameObject);
            
            pooledObject.name = "Wood Log";
            pooledObject.SetActive(false);
            return pooledObject;
        }

        // Called when an item is taken from the pool.
        private void OnGet(GameObject pooledObject)
        {
            pooledObject.SetActive(true);
        }

        // Called when an item is returned to the pool.
        public void OnRelease(GameObject pooledObject)
        {
            pooledObject.SetActive(false);
        }

        // Called when the pool decides to destroy an item (e.g., above max size).
        private void OnDestroyItem(GameObject pooledObject)
        {
            Destroy(pooledObject);
        }

        private System.Collections.IEnumerator ReturnAfter(GameObject pooledObject, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            // Give it back to the pool.
            _pool.Release(pooledObject);
        }
    }

