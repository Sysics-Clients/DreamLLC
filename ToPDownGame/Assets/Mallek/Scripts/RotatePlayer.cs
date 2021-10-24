using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    private float touch;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            touch = Input.mousePosition.x;
        }
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x != touch)
            {
                
                transform.Rotate(Vector3.up, -(Input.mousePosition.x-touch )* speed*Time.deltaTime);
                touch = Input.mousePosition.x;
            }
        }
        ////////////
        ///
    }
}
