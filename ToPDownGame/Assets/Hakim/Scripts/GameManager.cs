using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject MiniMapObjectDirection;
    public SpriteRenderer MiniMapDirectionSprite;
    public static WeopenType weopenType;
    public Level currentLevel;
    public Mission currentMission;
    public List<Level> Levels;
    public MissionObjects[] MissionObjects;
    public PlayerBehavior.PlayerPos playerPos;
    public GameObject InputSystem;

    private void OnEnable()
    {
        GeneralEvents.checkMissionCompletion += CheckMissiionCompletion;
        GeneralEvents.setMissionObjectAndSprite += SetMissionSpriteDirection;
    }
    private void OnDisable()
    {
        GeneralEvents.checkMissionCompletion -= CheckMissiionCompletion;
        GeneralEvents.setMissionObjectAndSprite -= SetMissionSpriteDirection;
    }
    private void Awake()
    {
        
        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance précédente (sécurité...sécurité...)
        instance = this;
        currentLevel = Levels[0];
        MissionObjects = FindObjectsOfType<MissionObjects>();
        foreach (Mission m in currentLevel.missions)
        {
           foreach(MissionObjects missionObjects in MissionObjects)
            {
                if (m.missionName == missionObjects.missionName && m.missionId == missionObjects.id)
                {
                    m.MissionObject = missionObjects.gameObject;
                    break;
                }
            }
        }
        GeneralEvents.changePlayerPos(playerPos);
        


    }
    private void Start()
    {
          InputSystem = GameObject.Find("CanvasInput (1)");
          InputSystem.GetComponent<Canvas>().enabled = true;
        GeneralEvents.startBullets();
    }
    public void SelectLevel(string sceneName)
    {
        foreach (Level l in Levels)
        {
            if (l.SceneName == sceneName)
            {
                currentLevel = l;
            }
        }
    }
    public bool CheckMissiionCompletion(MissionName missionName,int id=0)
    {
        foreach(Mission mission in currentLevel.missions)
        {
            if ((mission.missionName == missionName) && (mission.missionId == id))
            {
                if (mission.isCompleted)
                    return true;
                else 
                    return false;
            }
        }
        return false;
    }
    public void SetMissionSpriteDirection(GameObject obj=null,Sprite sprite=null)
    {
        if ((obj != null) && (sprite != null)) {
            MiniMapDirectionSprite.sprite = sprite;
            MiniMapObjectDirection.SetActive(true);
            MiniMapObjectDirection.GetComponent<RotateSprite>().iPad = obj.transform;
        }
        else
        {
            MiniMapObjectDirection.SetActive(false);
        }
    }

}
/*[System.Serializable]
public class MissionMessage
{
    public bool isCompleted;
    public string missionText;
    public MissionName missionName;
}
public enum MissionName { destroybox,collectPad}*/