using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingController : MonoBehaviour
{
    public List<GameObject> DesactivePanels;

    public GameObject VideoScreen;
    public void init()
    {
        VideoScreen.SetActive(true);
        foreach (var item in DesactivePanels)
        {
            item.SetActive(false);
        }
    }
}
