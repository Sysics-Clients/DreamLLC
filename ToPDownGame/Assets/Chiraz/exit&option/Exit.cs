using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void quitGame()
    {
        Application.Quit();
    }
    public void PlayAgain()
    {
        Singleton._instance.Level = 0;
        Singleton._instance.save();
        SceneManager.LoadScene(2);
    }
}
