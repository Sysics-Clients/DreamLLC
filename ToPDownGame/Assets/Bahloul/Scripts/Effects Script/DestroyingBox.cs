using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyingBox : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
