using System.Collections;
using UnityEngine;

public class FlameTrap : MonoBehaviour
{
  [SerializeField] GameObject flames;
  private bool _once = true;
  private void OnTriggerEnter(Collider collision)
  {
    if (collision.gameObject.CompareTag("Player")&& _once)
      StartCoroutine(Timer());
   
  }

  IEnumerator Timer()
  {
    flames.SetActive(true);
    _once = false;
    yield return new WaitForSeconds(3f);
    flames.SetActive(false);
    _once = true;
  }
  
}
