using System;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator =  GetComponentInParent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        animator.SetBool("Pushing", true);
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("Pushing", false);
    }
}
