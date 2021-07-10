using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByeBye : MonoBehaviour
{
    // Start is called before the first frame update
    public Behavior behavior;

    private void OnEnable()
    {
        behavior.sayhi += say;
    }

    private void OnDisable()
    {
        if (GeneralEvents.ennemyDown!=null)
        {
            GeneralEvents.ennemyDown(gameObject.name);
        }
        behavior.sayhi -= say;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
           
            if (behavior.bye!=null)
            {
                behavior.bye("Hakim");
            }
        }
    }

    void say()
    {
        print("Hi");
    }
}
