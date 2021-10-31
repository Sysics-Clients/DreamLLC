using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    private bool opened=false;
    public Sprite lockedDoor;
    public Sprite unlockedDoor;
    public Image DoorImage;
    public Button DoorBtn;
    public bool islocked;
    public bool withKey;
    public bool forMission;
    public GameObject InputPanel;
    public GameObject loadingScreenGameObject;
    private LoadingScreen loadingScreen;
    public string NewSceneName;
    public Doors DoorType;
    GameObject Player;
    GameManager gameManager;
    
    public enum Doors{
        rotator,
        translator,
    }
    // Start is called before the first frame update
    void Start()
    {
        if (loadingScreenGameObject != null)
        {
            loadingScreen = loadingScreenGameObject.GetComponent<LoadingScreen>();
            loadingScreenGameObject.SetActive(false);
        }
        Player = GameObject.FindGameObjectWithTag("Player");
        if (islocked)
        {
            DoorImage.sprite = lockedDoor;
            DoorBtn.enabled = false;
        }
        else
            DoorImage.sprite = unlockedDoor;
        DoorImage.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void OnDoorClick()
    {
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (opened)
                return;
            
            if (withKey)
            {
                if (!GeneralEvents.checkMissionCompletion(MissionName.collectAccessCard))
                {
                    GeneralEvents.writeErrorMessage("Door locked security key required!",Color.red);
                    return;
                }
            }
            if (NewSceneName == "")
            {
                opened = true;
                if (DoorType == Doors.rotator)
                {
                    transform.DORotate(new Vector3(0, transform.rotation.eulerAngles.y - 90, 0), 2);
                }
                else
                    transform.DOMoveX(transform.position.x + 3, 2);

                GeneralEvents.checkMissionCompletion(MissionName.openDoor, 0);

            }
            else
            {
                if (!GeneralEvents.testAllCompletion(MissionName.openDoor))
                {
                    GeneralEvents.writeErrorMessage("Finish All your missions first",Color.red);
                    return;
                }
                GeneralEvents.toNewScene(NewSceneName);
            }


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            
            GeneralEvents.hideErreurMessage();

        }
    }

}
