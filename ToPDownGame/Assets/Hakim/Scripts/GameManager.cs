using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject MiniMapObjectDirection;
    public SpriteRenderer MiniMapDirectionSprite;
    public static ItemTypes weopenType;
    public Level currentLevel;
    public Mission currentMission;
    public List<Level> Levels;
    public MissionObjects[] MissionObjects;
    public PlayerBehavior.PlayerPos playerPos;
    public GameObject InputSystem;
    public GameObject ContentTasks;
    public GameObject task;
    public List<GameObject> MiniMapTasks;
    public GameObject DontDestroyObject;
    public GameObject pad;
    public GameObject loadingScreenGameObject;
    LoadingScreen loadingScreen;
    public Health playercontroller;
    public GameObject GameOver;
    public ObjectActivation objectActivation;
    public GameObject GameWin;

    public void GoToNewScene(string NewSceneName)
    {
        InputSystem.GetComponent<Canvas>().enabled = false;
       // loadingScreenGameObject.SetActive(true);
        // gameManager.DontDestroyObjects();
       // loadingScreen.sceneName = NewSceneName;
       // loadingScreen.ToScene = true;
    }

    private void OnEnable()
    {
        GeneralEvents.toNewScene += GoToNewScene;
        GeneralEvents.checkMissionCompletion += CheckMissiionCompletion;
        GeneralEvents.setMissionObjectAndSprite += SetMissionSpriteDirection;
        GeneralEvents.testAllCompletion += testAllCompletion;
    }
    private void OnDisable()
    {
        GeneralEvents.toNewScene -= GoToNewScene;
        GeneralEvents.checkMissionCompletion -= CheckMissiionCompletion;
        GeneralEvents.setMissionObjectAndSprite -= SetMissionSpriteDirection;
        GeneralEvents.testAllCompletion -= testAllCompletion;
    }
    public void DontDestroyObjects()
    {
        DontDestroyOnLoad(DontDestroyObject);
    }
    private void Awake()
    {
        
        /*if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance précédente (sécurité...sécurité...)*/
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
            GameObject obj=Instantiate(task, ContentTasks.transform);
            MissionObjects mo = obj.GetComponent<MissionObjects>();
            mo.missionName = m.missionName;
            mo.id = m.missionId;
            obj.transform.GetChild(0).GetComponent<Text>().text = m.missionText;
            MiniMapTasks.Add(obj);
            if (Levels.Count > 0)
            {
                foreach (var item in Levels[0].missions)
                {
                    item.isCompleted = false;
                }
            }
        }
    }
    private void Update()
    {
        if (GameOver.activeInHierarchy||GameWin.activeInHierarchy)
        {
            return;
        }
        if (playercontroller!=null)
        {
            if (playercontroller.corentHelth<=0)
            {
                if (!GameOver.gameObject.activeInHierarchy)
                {
                    GameOver.SetActive(true);
                }
            }
        }
        if (objectActivation!=null)
        {
            if (currentLevel!=null)
            {
                int count = currentLevel.missions.Count;
                int index = 0;
                foreach (var item in currentLevel.missions)
                {
                    if (item.isCompleted)
                    {
                        index++;
                    }
                }
                if (count==index)
                {
                    foreach (var item in objectActivation.DroneEnemyList)
                    {
                        if (item!=null)
                        {
                            Destroy(item.gameObject);
                        }
                    }
                    foreach (var item in objectActivation.FullEnemyList)
                    {
                        if (item != null)
                        {
                            Destroy(item.gameObject);
                        }
                    }
                    foreach (var item in objectActivation.SniperEnemyList)
                    {
                        if (item != null)
                        {
                            Destroy(item.gameObject);
                        }
                    }
                    GameWin.SetActive(true);
                }
            }
        }
    }
    public bool testAllCompletion(MissionName mission=MissionName.NoMissionAvailale)
    {
        foreach(Mission m in currentLevel.missions)
        {
            if (m.missionName == mission)
            {
                continue;
            }
            if (m.isCompleted == false)
            {
                return false;
            }
        }
        return true;
    }
    private void Start()
    {
        GeneralEvents.changePlayerPos(playerPos);
        InputSystem = GameObject.Find("CanvasInput (1)");
        InputSystem.GetComponent<Canvas>().enabled = true;
        loadingScreen = loadingScreenGameObject.GetComponent<LoadingScreen>();
        loadingScreenGameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            pad = GameObject.Find("Pad");
            pad.SetActive(false);
        }
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