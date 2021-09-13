using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyingBoxPieces : MonoBehaviour
{
     private float timeToWait;
    private void Start()
    {
        StartCoroutine(WaitToDestroy());
    }
    IEnumerator WaitToDestroy() {
        timeToWait=Random.Range(6, 8);
        yield return new WaitForSeconds(timeToWait);
        Destroy(gameObject);
    }
}
