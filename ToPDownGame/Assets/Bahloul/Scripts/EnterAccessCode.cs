using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterAccessCode : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GeneralEvents.checkMissionCompletion(MissionName.CollectCodePaper, 0))
            {
                GeneralEvents.enableSD();
            }
            else
            {
                GeneralEvents.writeErrorMessage("You Have To Read the code Paper!!", Color.red);
                GeneralEvents.hideErreurMessage(4);
            }
        }
    }
}
