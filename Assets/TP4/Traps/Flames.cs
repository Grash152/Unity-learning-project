using System;
using System.Collections;
using UnityEngine;

public class Flames : MonoBehaviour
{
    private bool _dot = true;
    private bool _once = true;
    private SkinnedMeshRenderer sr;
    [SerializeField] private GameObject Body;

    private void OnEnable()
    {
        _dot = true;
        _once = true;
        sr = Body.GetComponent<SkinnedMeshRenderer>();
    }

    private void OnTriggerStay(Collider other)
    {
      

        if (other.gameObject.CompareTag("Player") && _dot)
        {
            
            StartCoroutine(TakeDamage(false));
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player")&&_once)
        {
            
            StartCoroutine(TakeDamage(true));
        }
    }

    private void OnDisable()
    {
        if(sr !=null)
            sr.material.color = Color.white;
    }
    IEnumerator TakeDamage(bool over)
    {
        if (!over)
        {
            _dot = false;
            
            HPSystem.SetCurrentHp(HPSystem.GetCurrentHp()-2);
            if(sr !=  null)
                sr.material.color = Color.red;
            yield return new WaitForSeconds(1f);
            if(sr !=  null)
                sr.material.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            _dot = true;
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                _once = false;
                HPSystem.SetCurrentHp(HPSystem.GetCurrentHp()-5);
                if(sr !=  null)
                    sr.material.color = Color.red;
                yield return new WaitForSeconds(1f);
                if(sr !=  null)
                    sr.material.color = Color.white;
                yield return new WaitForSeconds(0.2f);
                
            }
        }
        
        
 
    }
}
