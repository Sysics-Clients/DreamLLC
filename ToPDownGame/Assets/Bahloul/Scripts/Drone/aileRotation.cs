using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aileRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float RotSpeed = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, RotSpeed, 0));
    }
}
