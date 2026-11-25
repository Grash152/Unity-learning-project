using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouvement : MonoBehaviour
{
    public float _acceleration;
    public float _deceleration;
    public static Vector3 Velocity;
    
    private PlayerInput playerInput;
    private InputAction walkAction;
    private InputAction sprintAction;
    private Vector2 lookAction;
    private Vector2 v2;
    private Vector3 direction;
 
    private CharacterController controller;
    private Animator animator;
    [SerializeField] private int walkspeed;
    [SerializeField] private int sprintspeed;
    [SerializeField] private float rotatespeed;
    [SerializeField] private float sprintrotatespeed;
    
    private GameObject mainCamera;
    private float targetYaw;
    private float targetPitch;
    private const float threshold = 0.01f;
    CharacterController cc;
   
    
    public GameObject cameraTarget;
    public float topClamp = 70.0f;
    public float bottomClamp = -30.0f;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        walkAction = playerInput.actions["Move"];
        sprintAction = playerInput.actions["Sprint"];
        controller = GetComponent<CharacterController>();
        animator =  GetComponent<Animator>();
        mainCamera = GameObject.Find("Main Camera");
        cc = GetComponent<CharacterController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetYaw = cameraTarget.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputValue();
        Move();
        SetRotation();
        SetAnimationState();
    }

    void LateUpdate()
    {
        //CameraRotation();
    }

    private void Move()
    {
        if (v2 != Vector2.zero)
        {
            float _targetRotation = Mathf.Atan2(v2.x, v2.y) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            direction = Quaternion.Euler(0,_targetRotation, 0) * Vector3.forward;
            if (_acceleration <= 0f)
                _acceleration = 1f;
            if (sprintAction.IsPressed())
            {
                if (_acceleration <= sprintspeed)
                    _acceleration += _acceleration * Time.deltaTime;
                
            }
            else
            {
                if(_acceleration > walkspeed)
                    _acceleration -= _deceleration * Time.deltaTime;
                else if (_acceleration <= walkspeed)
                    _acceleration += _acceleration * Time.deltaTime*0.75f;
            }
        }
        else
        {
            if(_acceleration > 10f)
                _acceleration -= _deceleration * Time.deltaTime*5f;
            else if(_acceleration > walkspeed)
                _acceleration -= _deceleration * Time.deltaTime *1.5f;
            else
            {
                _acceleration -= _deceleration * Time.deltaTime ;
            }
            if(_acceleration <= 0f)
                _acceleration = 0f;
        }

        Velocity = direction * _acceleration;
        if (cc.enabled)
        {
            if (sprintAction.IsPressed())
                controller.SimpleMove(Velocity * sprintspeed);
            else
                controller.SimpleMove(Velocity * walkspeed);
        }
    }

    private void SetAnimationState()
    {
        animator.SetFloat("Speed",_acceleration);
    }

    private void GetInputValue()
    {
        v2 = walkAction.ReadValue<Vector2>();
    }

    private void SetRotation()
    {
        if (sprintAction.IsPressed())
        {
            if (controller.velocity.magnitude  > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                if (transform.rotation != targetRotation)
                {
                    transform.rotation =
                        Quaternion.Lerp(transform.rotation, targetRotation, sprintrotatespeed * Time.deltaTime);
                }
            }
        }
        else
        {
            if (controller.velocity.magnitude   > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                if (transform.rotation != targetRotation)
                {
                    transform.rotation =
                        Quaternion.Lerp(transform.rotation, targetRotation, rotatespeed * Time.deltaTime);
                }
            }
        }
    }

    private void CameraRotation()
    {
        lookAction = playerInput.actions["Look"].ReadValue<Vector2>();
        if (lookAction.sqrMagnitude >= threshold)
        {
            targetYaw += lookAction.x * 0.3f;
            targetPitch += lookAction.y * -1 * 0.3f;
        }
        targetYaw = ClampAngle(targetYaw, float.MinValue, float.MaxValue);
        cameraTarget.transform.rotation = Quaternion.Euler(targetPitch, targetYaw, 0.0f);
    }
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if(angle> 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}