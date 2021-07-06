using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayHi : MonoBehaviour
{
    // Start is called before the first frame update
    public Behavior behavior;

    private void OnEnable()
    {
        behavior.bye += bye;
    }
    private void OnDisable()
    {
        behavior.bye -= bye;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {

            if (behavior.sayhi != null)
            {
                behavior.sayhi();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (GeneralEvents.addCounter!=null)
            {
                GeneralEvents.addCounter();
            }
        }
    }

    void bye(string n)
    {
        print(n);
    }
}
