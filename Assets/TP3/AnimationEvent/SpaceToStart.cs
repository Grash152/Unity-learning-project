using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceToStart : MonoBehaviour
{
    private Animator _animator;
    private Renderer _water;
    private Material _material;

    [SerializeField] private GameObject fire;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject water;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _water = water.GetComponent<Renderer>();
        _material = _water.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        { 
            _material.SetFloat("Vector1_36E8227", 0.4f);
            _animator.SetBool("Start",true);
            fire.SetActive(true);
            text.text = " ";
            
        }

    }
}
