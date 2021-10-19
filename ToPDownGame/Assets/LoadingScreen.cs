using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour
{
    public enum TypeOfLoading
    {
        Loading,
        Saving,
    }
    public bool ToScene;
    public string sceneName;
    public TypeOfLoading typeOfLoading;
    [Header ("Loading Objects")]
    public Image bar;
    public Text percentageText;
    public GameObject loadingPanel;
    [Header("Saving Objects")]
    public GameObject MainMenu;
    public GameObject savingPanel;
    public Image savingIcon;
    public bool isWin;
    void Start()
    {
        /*if (typeOfLoading == TypeOfLoading.Loading)
        {
            StartCoroutine(caroutineBar());
            loadingPanel.SetActive(true);
}
        else
        {
            StartCoroutine(CouroutineSave());
            savingPanel.SetActive(true);
        }*/
        StartCoroutine(CouroutineSave());
        savingPanel.SetActive(true);
    }
    IEnumerator CouroutineSave()
    {
        Time.timeScale = 1;
        print("heyyy");
        Time.timeScale = 1;
        savingIcon.transform.DORotate(new Vector3(0, 0, 180),0.5f).SetLoops(-1, LoopType.Incremental);
        Time.timeScale = 1;
        yield return new WaitForSeconds(2);
        print("heyyy");

        int index = SceneManager.GetActiveScene().buildIndex;
        if (isWin)
        {
            SceneManager.LoadScene(index + 1);
        }
        else
        {
            SceneManager.LoadScene(index);
        }
        print("heyyy");


    }
    IEnumerator caroutineBar()
    {
        percentageText.text = "0%";
        int amount = 0;
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 20; i++)
        {
            bar.fillAmount += 0.01f;
            amount++;
            percentageText.text = amount + "%";
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < 20; i++)
        {
            bar.fillAmount += 0.01f;
            amount++;
            percentageText.text = amount + "%";
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 20; i++)
        {
            bar.fillAmount += 0.01f;
            amount++;
            percentageText.text = amount + "%";
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 20; i++)
        {
            bar.fillAmount += 0.01f;
            amount++;
            percentageText.text = amount + "%";
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 20; i++)
        {
            bar.fillAmount += 0.01f;
            amount++;
            percentageText.text = amount + "%";
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(1);
        if (ToScene)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            MainMenu.SetActive(true);
            this.gameObject.SetActive(false);
        }

    }
}
