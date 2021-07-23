using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float damege;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //print(other.transform.name);
        if (other.transform.tag == "enemy")
        {
            other.gameObject.GetComponent<EnemyBehavior>().takeDamage(damege);
        }
    }
}
