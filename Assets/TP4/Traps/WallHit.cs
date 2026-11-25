using System;
using System.Collections;

using UnityEngine;

public class WallHit : MonoBehaviour
{
    private SkinnedMeshRenderer sr;
    [SerializeField] private GameObject Body;
    [SerializeField] private GameObject wind;
    private bool once = true;
    private void Start()
    {
        sr = Body.GetComponent<SkinnedMeshRenderer>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && once && wind.activeInHierarchy)
        {
            HPSystem.SetCurrentHp(HPSystem.GetCurrentHp()-10);
            StartCoroutine(Hurt());
        }
    }
    IEnumerator Hurt()
    {
        once = false;
        if(sr !=  null)
            sr.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        if(sr !=  null)
            sr.material.color = Color.white;
        yield return new WaitForSeconds(5f);
        once = true;
    }
}
