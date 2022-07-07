using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    
    Collider damege;

    private void OnEnable()
    {
        GeneralEvents.sendShooting += stab;
    }
    private void OnDisable()
    {
        GeneralEvents.sendShooting -= stab;
    }
    // Start is called before the first frame update
    void Start()
    {
        damege = GetComponent<Collider>();
        damege.enabled = false;
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
            other.gameObject.GetComponent<EnemyBehavior>().takeDamage(1000);
        }
    }

    void stab(Vector3 v)
    {
        if(v!=Vector3.zero)
            StartCoroutine("attack");
    }
    IEnumerator attack()
    {
        yield return new WaitForSeconds(0.2f);
        damege.enabled = true;
        yield return new WaitForSeconds(1);
        damege.enabled = false;
    }

}
