using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private ForceMode fm;
    [SerializeField] private float pushforce;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Rigidbody ragdollHips;
    [SerializeField] Slider _healthBar;
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject cam;

    private Vector3 direction;
    private Animator animator;
    private Rigidbody rb;
    private bool istouching;
    private Rigidbody[] ragdollRigidbodies;
    private CharacterController cc;
    private Mouvement boost;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator =  GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }
    void Start()
    {
        HPSystem.SetCurrentHp(HPSystem.GetMaxHp());
        _healthBar.maxValue = HPSystem.GetMaxHp();
        _healthBar.value = HPSystem.GetCurrentHp();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        SetRagdollState(false);
    }
    private void UpdateHealth()
    {
        _healthBar.value = HPSystem.GetCurrentHp();
    }

    private void Update()
    {
        UpdateHealth();
        if (HPSystem.GetCurrentHp() <= 0)
        {
            GoRagdoll();
        }

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            if(soundMenu.activeSelf)
            {
                soundMenu.SetActive(false);
                cam.GetComponent<CinemachineInputAxisController>().enabled = true;
                GetComponent<CharacterController>().enabled = true;
            }
            else if (!soundMenu.activeSelf)
            {
                soundMenu.SetActive(true);
                cam.GetComponent<CinemachineInputAxisController>().enabled = false;
                GetComponent<CharacterController>().enabled = false;
            }

        }
        
    }
    public void GoRagdoll()
    {
        playerInput.currentActionMap.Disable();
        if (animator != null) animator.enabled = false;
        SetRagdollState(true);
    }

    public void EndRagdoll()
    {
        playerInput.currentActionMap.Enable();
        if (animator != null) animator.enabled = true;
        SetRagdollState(false);
    }
    public Rigidbody GetRagdollHips()
    {
        return ragdollHips;
    }
    private void SetRagdollState(bool state)
    {
        cc.enabled = !state;
        foreach (var rb in ragdollRigidbodies)
        {
            rb.isKinematic = !state;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        boost = GetComponent<Mouvement>();
        direction = collision.moveDirection;
        direction.y = 0;
        rb = collision.collider.attachedRigidbody;
        if (collision.normal.y <  0.7f)
        { 
            istouching = true;
        }

        if (rb == null || rb.isKinematic)
            return;
        if (collision.collider.tag == "Boost")
        {
            boost._acceleration += 1;
            if (boost._acceleration >= 20)
            {
                boost._acceleration = 20;
            }
        }
   

        if (collision.gameObject.tag == "Spin")
        {
            rb.AddTorque(Vector3.up * pushforce,fm);
        }

        if (collision.gameObject.tag == "Push")
        {
            rb.AddForce(direction * pushforce, fm);
        }
        if (collision.collider.tag == "Floor")
        {
            collision.collider.GetComponent<Renderer>().material.color = Color.red;
            collision.collider.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            collision.gameObject.layer = LayerMask.NameToLayer("FallingTiles");
        }
        
    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (cc.enabled == false)
        {
            Invoke(nameof(EndRagdoll), 6f);
        }

        animator.SetBool("Pushing", istouching);
      
        istouching = false;
      
    }
   
}
