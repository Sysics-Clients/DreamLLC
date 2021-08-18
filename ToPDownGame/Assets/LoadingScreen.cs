using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour
{
    public Image bar;
    public Text percentageText;
    public GameObject MainMenu;
    public bool ToScene;
    public string sceneName;
    void Start()
    {
        StartCoroutine(caroutineBar());
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
