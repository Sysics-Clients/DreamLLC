using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "New Level", menuName = "Create Level")]
public class Level : ScriptableObject
{
    
    public List<Mission> missions;
    public string SceneName;
}
[System.Serializable]
public class Mission
{
    public bool isCompleted;
    public string missionText;
    public MissionName missionName;
    public GameObject MissionObject;
    public Sprite MissionSprite;
    public short priority;
    public short missionId;
}

public enum MissionName { destroybox, collectPad,destroyEnemy,collectAccessCard,openDoor,switchToPistol,TryToRoll,NoMissionAvailale }
