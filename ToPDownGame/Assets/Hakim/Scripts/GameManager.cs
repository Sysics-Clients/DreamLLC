using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static WeopenType weopenType;
    public List<MissionMessage> missionMessages;
    
   
}
[System.Serializable]
public class MissionMessage
{
    public bool isCompleted;
    public string missionText;
    public MissionName missionName;
}
public enum MissionName { destroybox,collectPad}