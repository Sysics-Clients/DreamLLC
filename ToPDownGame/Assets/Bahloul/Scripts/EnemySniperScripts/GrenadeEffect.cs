using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeEffect : MonoBehaviour
{ public GameObject effectFreeze;
    public GameObject sender;
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
        if((other.gameObject.tag=="Player")||(other.gameObject.tag == "Ground"))
        {
            SniperBehavior sniperBehavior = sender.GetComponent<SniperBehavior>();
            if (sniperBehavior != null)
                Instantiate(effectFreeze, sniperBehavior.Player.transform);
            else
                Instantiate(effectFreeze, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
