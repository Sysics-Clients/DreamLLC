using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollecteCard : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(waitToActive());
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        
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
            GeneralEvents.onTaskFinish(gameObject.GetComponent<MissionObjects>().missionName, gameObject.GetComponent<MissionObjects>().id);
            Destroy(gameObject);
        }
    }
}
