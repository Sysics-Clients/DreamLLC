using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "New Level", menuName = "Create Level")]
public class Level : ScriptableObject
{
    
    public List<Mission> missions;
    public string SceneName;
    public void AddMission(MissionName mn, int id)
    {
        missions.Add(new Mission(mn, id));
    }
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
    public Mission(MissionName mn,int id)
    {
        missionName = mn;
        id = missionId;
    }
}

public enum MissionName { destroybox, collectPad,destroyEnemy,collectAccessCard,openDoor,switchToPistol,TryToRoll,NoMissionAvailale }
