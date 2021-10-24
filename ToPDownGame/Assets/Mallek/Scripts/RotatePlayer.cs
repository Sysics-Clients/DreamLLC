using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    private float touch;
    public float speed;
    bool canrotate;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            touch = Input.mousePosition.x;
            if (touch<=Screen.width/3)
            {
                canrotate = true;
            }
            else
            {
                canrotate = false;
            }
        }
        if (Input.GetMouseButton(0)&&canrotate)
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
