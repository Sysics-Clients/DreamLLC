using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Launcher : MonoBehaviour
{
    public string url;
    public GameObject panel;
    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("read"))
        {
            panel.SetActive(false);
            LoadScene();
        }
        else
        {
            panel.SetActive(true);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenUrl()
    {
        Application.OpenURL(url);
    }

    public void Accept()
    {
        panel.SetActive(false);
        PlayerPrefs.SetFloat("read", 1);
        SceneManager.LoadScene(1);

    }
}
