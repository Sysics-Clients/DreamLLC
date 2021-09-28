using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    public Sprite lockedDoor;
    public Sprite unlockedDoor;
    public Image DoorImage;
    public Button DoorBtn;
    public bool islocked;
    public GameObject dontDestroyObjects;
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
        if (NewSceneName == "")
        {
            if (DoorType == Doors.rotator)
            {
                transform.DORotate(new Vector3(0, 90, 0), 2);
            }
            else
                transform.DOMoveX(transform.position.x + 3, 2);
        }
        else
        {
            if (GeneralEvents.checkMissionCompletion(MissionName.collectAccessCard))
            {
                InputPanel.GetComponent<Canvas>().enabled = false;
                loadingScreenGameObject.SetActive(true);
                DontDestroyOnLoad(dontDestroyObjects);
                loadingScreen.sceneName = NewSceneName;
                loadingScreen.ToScene = true;
                Player.GetComponent<PlayerBehavior>().changePos(PlayerBehavior.PlayerPos.Kitchen);
            }
            else
            {
                GeneralEvents.writeErrorMessage("No key found!!");
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            DoorImage.enabled = true;
            
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DoorImage.enabled = false;

        }
    }

}
