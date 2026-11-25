using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WindTrap : MonoBehaviour
{
    [SerializeField] GameObject wind;
    private bool redo = true;
    private void OnTriggerEnter(Collider other)
    {
        if (redo)
        {
            StartCoroutine(Delay());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (redo)
        {
            StartCoroutine(Delay());    
        }
    }

    IEnumerator Delay()
    {
        redo = false;
        wind.SetActive(true);
        yield return new WaitForSeconds(5f);
        wind.SetActive(false);
        yield return new WaitForSeconds(10f);
        redo = true;
        
    }
}
