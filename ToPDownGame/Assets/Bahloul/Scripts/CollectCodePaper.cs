using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCodePaper : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GeneralEvents.onTaskFinish(gameObject.GetComponent<MissionObjects>().missionName, gameObject.GetComponent<MissionObjects>().id);
            GeneralEvents.newAccessCode();
            Destroy(gameObject);
        }
    }
}
