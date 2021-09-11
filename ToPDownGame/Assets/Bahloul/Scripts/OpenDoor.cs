using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    public GameObject dontDestroyObjects;
    public GameObject InputPanel;
    public GameObject loadingScreenGameObject;
    private LoadingScreen loadingScreen;
    public string NewSceneName;
    public Doors DoorType;
    GameObject Player;
    
    public enum Doors{
        rotator,
        translator,
    }
    // Start is called before the first frame update
    void Start()
    {
        loadingScreen = loadingScreenGameObject.GetComponent<LoadingScreen>();
        loadingScreenGameObject.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
        
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
        }
        else
        {
            Player.GetComponent<PlayerBehavior>().changePos(PlayerBehavior.PlayerPos.Kitchen1);
            InputPanel.SetActive(false);
            loadingScreenGameObject.SetActive(true);
            DontDestroyOnLoad(dontDestroyObjects);
            loadingScreen.sceneName = NewSceneName;
            loadingScreen.ToScene = true;
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            InputPanel.GetComponent<InputSystem>().OpenDoorIcon.enabled = true;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            InputPanel.GetComponent<InputSystem>().OpenDoorIcon.enabled = false;

        }
    }

}
