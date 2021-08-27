using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollecteCard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(waitToActive());
    }
    IEnumerator waitToActive()
    {
        
        yield return new WaitForSeconds(2);

        GetComponent<MeshRenderer>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
