using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class TrainingLevel : MonoBehaviour
{
    //public List<TrainingMission> missions;
    public Level trainingLevel;
    InputSystem inputSystem;
    GameObject InputSystemCanvas;
    public TrainingMissionsName currentMission;
    public GameObject MissionBar;
    public GameObject ShootJoystic;
    public GameObject MoveJoystic;
    public GameObject miniMap;
    public GameObject AkButton;
    public GameObject pistolButton;
    public GameObject WalkStealthButton;
    public GameObject hideButton;
    public GameObject RollButton;
    public GameObject HealthSlider;
    public GameObject ShieldSlider;
    public GameObject PlayerState;
    public GameObject TrainingPanel;
    public SpriteRenderer MiniMapImage;
    public RectTransform TrainingImage;
    public GameObject _TrainingImage;
    public RectTransform ArrowImage;
    public Text TrainingMsg;
    public GameObject EventSystem;
    public GameObject CongratsPanel;
    public bool isEnded=false;


    // Start is called before the first frame update
    void Start()
    {
        InputSystemCanvas = GameObject.Find("CanvasInput (1)");
        inputSystem = InputSystemCanvas.GetComponent<InputSystem>();

        MoveJoystic.SetActive(false);
        ShootJoystic.SetActive(false);
        miniMap.SetActive(false);
        AkButton.SetActive(false);
        pistolButton.SetActive(false);
        WalkStealthButton.SetActive(false);
        hideButton.SetActive(false);
        RollButton.SetActive(false);
        HealthSlider.SetActive(false);
        ShieldSlider.SetActive(false);
        MiniMapImage.enabled = false;
        ToMissionBar();
        CongratsPanel.SetActive(false);
        StartCoroutine(waitToDesactivateEventSystem());
    }
    IEnumerator waitToDesactivateEventSystem()
    {
        yield return new WaitForSeconds(0.5f);
        EventSystem.SetActive(false);
    }
    void setPos(RectTransform rect,Vector2 vect)
    {
        TrainingImage.anchorMax = rect.anchorMax;
        TrainingImage.anchorMin = rect.anchorMin;
        TrainingImage.anchoredPosition = vect;
        TrainingImage.transform.DOScaleX(1, 2f).SetEase(Ease.Linear);
    }
    void ToMissionBar()
    {
        setPos(PlayerState.GetComponent<RectTransform>(), new Vector2(370,-35));
        TrainingMsg.text = "Here is your missions That you have to do!";
        currentMission = TrainingMissionsName.MissionsBar;
        //TrainingImage.localPosition = new Vector2(-35, 228);
        
    }
    void ToHealthBarTraining()
    {
        TrainingMsg.text = "Here is your health! if you lose too much health, you will die!";
        currentMission = TrainingMissionsName.HealthBar;
        //TrainingImage.localPosition = new Vector2(-107, 192);
        HealthSlider.SetActive(true);
        ShieldSlider.SetActive(true);
        setPos(PlayerState.GetComponent<RectTransform>(), new Vector2(335, -77));
    }
    void ToMoveJoystic()
    {
        TrainingMsg.text = "Use this joystic to reach the Pad!";
        currentMission = TrainingMissionsName.MoveJoystic;
        //TrainingImage.localPosition = new Vector2(-119, -82);
        setPos(MoveJoystic.GetComponent<RectTransform>(), new Vector2(300, 179));
        MoveJoystic.SetActive(true);
    }
    void ToMiniMap()
    {
        TrainingMsg.text = "This is the minimap, Here you can see objects around!";
        currentMission = TrainingMissionsName.MiniMap;
        //TrainingImage.localPosition = new Vector2(-56, 186);
        ArrowImage.localPosition = new Vector2(331, 0);
        ArrowImage.localRotation =Quaternion.Euler( new Vector3(0, 0, -90) );
        miniMap.SetActive(true);
        setPos(miniMap.GetComponent<RectTransform>(), new Vector2(-510, -82));
    }
    void ToMiniMapSprite()
    {

        TrainingMsg.text = "This sprite shows you the direction you have to follow!";
        currentMission = TrainingMissionsName.MiniMapSprite;
        MiniMapImage.enabled = true;
        //setPos(MiniMapImage.GetComponent<RectTransform>(), new Vector2(300, 179));

    }
    void ToShootJoystic()
    {
        TrainingMsg.text = "Use this joystic to shoot the boxes! Your default gun is AK!";
        currentMission = TrainingMissionsName.shootJoystic;
        //TrainingImage.localPosition = new Vector2(-143, -88);
        ShootJoystic.SetActive(true);
        AkButton.SetActive(true);
        setPos(ShootJoystic.GetComponent<RectTransform>(), new Vector2(-594, 179));
    }
    void toSwitchGun()
    {
        TrainingMsg.text = "You can switch to pistol by this button!";
        currentMission = TrainingMissionsName.switchGun;
        //TrainingImage.localPosition = new Vector2(-276, -191);
        ArrowImage.localPosition = new Vector2(-28, 0);
        ArrowImage.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
        pistolButton.SetActive(true);
        setPos(pistolButton.GetComponent<RectTransform>(), new Vector2(170, 74));
    }
    void ToRoll()
    {
        currentMission = TrainingMissionsName.Roll;
        TrainingMsg.text = "This button allow you to roll! Try it";
        //TrainingImage.localPosition = new Vector2(-120, -203);
        ArrowImage.localPosition = new Vector2(331, 0);
        ArrowImage.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
        RollButton.SetActive(true);
        setPos(miniMap.GetComponent<RectTransform>(), new Vector2(-565, -430));

    }
    void FinishTraining()
    {
        CongratsPanel.SetActive(true);
        TrainingPanel.SetActive(false);
        isEnded = true;
    }
    public void OnContinueClick()
    {
        CongratsPanel.SetActive(false);
        GeneralEvents.onTaskFinish(MissionName.NoMissionAvailale);

    }
    public void OnLeaveClick()
    {
        //SceneManager.LoadScene("");
    }

    public void OnPistolButtonClick()
    {
        if(!GeneralEvents.checkMissionCompletion(MissionName.switchToPistol))
            GeneralEvents.onTaskFinish( MissionName.switchToPistol);
    }
    public void OnRollButtonClick()
    {
        if (!GeneralEvents.checkMissionCompletion(MissionName.TryToRoll))
            GeneralEvents.onTaskFinish(MissionName.TryToRoll);
    }
    private void FixedUpdate()
    {
        if (isEnded) return;
        switch (currentMission)
        {
            case TrainingMissionsName.MissionsBar:
                if (Input.GetMouseButtonDown(0))
                {
                    ToHealthBarTraining();
                }
                break;
            case TrainingMissionsName.HealthBar:
                if (Input.GetMouseButtonDown(0))
                {
                    ToMoveJoystic();
                }
                break;
            case TrainingMissionsName.MoveJoystic:
                if (Input.GetMouseButtonDown(0))
                {
                    TrainingPanel.SetActive(false);
                    EventSystem.SetActive(true);
                }
                if(GeneralEvents.checkMissionCompletion(MissionName.collectAccessCard))
                {
                    TrainingPanel.SetActive(true);
                    ToMiniMap();
                    MiniMapImage.enabled = false;
                    StartCoroutine(waitToDesactivateEventSystem());
                }
                break;
            case TrainingMissionsName.MiniMap:
                if (Input.GetMouseButtonDown(0))
                {
                    ToMiniMapSprite();
                }
                break;
                
            case TrainingMissionsName.MiniMapSprite:
                if (Input.GetMouseButtonDown(0))
                {
                    ToShootJoystic();
                }
                break;
            case TrainingMissionsName.shootJoystic:
                if (Input.GetMouseButtonDown(0))
                {
                    TrainingPanel.SetActive(false);
                    EventSystem.SetActive(true);
                }
                if (GeneralEvents.checkMissionCompletion(MissionName.destroybox))
                {
                    toSwitchGun();
                    TrainingPanel.SetActive(true);
                    StartCoroutine(waitToDesactivateEventSystem());
                }
                break;
            case TrainingMissionsName.switchGun:
                if (Input.GetMouseButtonDown(0))
                {
                    TrainingPanel.SetActive(false);
                    EventSystem.SetActive(true);
                    
                }
                if (GeneralEvents.checkMissionCompletion(MissionName.switchToPistol))
                {
                    ToRoll();
                    TrainingPanel.SetActive(true);
                    StartCoroutine(waitToDesactivateEventSystem());
                }
                break;
            case TrainingMissionsName.Roll:
                if (Input.GetMouseButtonDown(0))
                {
                    TrainingPanel.SetActive(false);
                    EventSystem.SetActive(true);
                    
                }
                if (GeneralEvents.checkMissionCompletion(MissionName.TryToRoll))
                {
                    
                    FinishTraining();
                }
                break;
        }
    }

    [System.Serializable]
    public enum TrainingMissionsName { MissionsBar,HealthBar,MoveJoystic,MiniMap,shootJoystic,MiniMapSprite,switchGun,Roll }

    public class TrainingMissions
    {

    }
}


