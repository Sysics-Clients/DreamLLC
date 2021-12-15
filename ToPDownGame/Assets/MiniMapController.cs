using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniMapController : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;

    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
