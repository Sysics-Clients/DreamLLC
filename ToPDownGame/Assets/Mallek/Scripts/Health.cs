using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    const int maxHealth=100;
    int corentHelth;
    // Start is called before the first frame update
    void Start()
    {
        corentHelth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void damege(int value)
    {
        corentHelth -= value;
    }
}
