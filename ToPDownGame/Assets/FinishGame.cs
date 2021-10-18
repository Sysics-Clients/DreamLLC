using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class FinishGame : MonoBehaviour
{
    public GameStatePanel gameStatePanel;
    public GameObject loadingScene;
    private void OnEnable()
    {
        if (gameStatePanel!=null)
        {
            StartCoroutine(startAnimation());
        }
    }

    IEnumerator startAnimation()
    {
        yield return new WaitForFixedUpdate();
        if (gameStatePanel!=null)
        {
            gameStatePanel.PanelState.transform.localScale = Vector3.zero;

            gameStatePanel.PanelState.gameObject.SetActive(true);
            gameStatePanel.PanelState.transform.DOScale(Vector3.one, 0.4f).SetEase(gameStatePanel.easeBG);
            yield return new WaitForSeconds(0.5f);
            if (gameStatePanel.rewards.Count > 0)
            {
                foreach (var item in gameStatePanel.rewards)
                {
                    item.activateObj();
                    yield return new WaitForSeconds(0.6f);

                }
                
            }
            gameStatePanel.Translate();


        }
    }
    public void loadScene(bool iswin)
    {

        loadingScene.SetActive(true);
        loadingScene.GetComponent<LoadingScreen>().isWin=iswin;
       
    }
}


[System.Serializable]
public class GameStatePanel
{
    public List<Reward> rewards;
    public Image PanelState;
    public Button adsButton;
    public Button collectButton;
    public Ease easeBG;

    public void Translate()
    {
        RectTransform adsrect = adsButton.GetComponent<RectTransform>();
        adsrect.position=new Vector3(adsrect.position.x-60, adsrect.position.y, adsrect.position.z);
        adsButton.image.DOFade(0, 0.0f);
        adsButton.gameObject.SetActive(true);
        adsrect.DOMoveX(adsrect.position.x + 60, 1).SetEase(easeBG);
        adsButton.image.DOFade(1, 0.3f);

        RectTransform collectrect = collectButton.GetComponent<RectTransform>();
        collectrect.position = new Vector3(collectrect.position.x + 60, adsrect.position.y, adsrect.position.z);
        collectButton.image.DOFade(0, 0.0f);
        collectButton.gameObject.SetActive(true);
        collectrect.DOMoveX(collectrect.position.x - 60, 1).SetEase(easeBG);
        collectButton.image.DOFade(1, 0.3f);


    }
}
[System.Serializable]
public class Reward
{
    public GameObject RewardObj;
    public Image icon;
    public Text textValue;
    public Ease ease;

    public void activateObj()
    {
        RewardObj.transform.localScale = Vector3.zero;
        textValue.gameObject.SetActive(false);
        RectTransform rectTransform = RewardObj.GetComponent<RectTransform>();
        Image img= RewardObj.GetComponent<Image>();
        img.color = new Color(255, 255, 255, 0);
        icon.DOFade(0, 0);
        RewardObj.SetActive(true);
        RewardObj.transform.DOScale(Vector3.one, 0.4f).SetEase(ease);
        img.DOFade(1, 0.8f);
        icon.DOFade(1, 0.4f);
        textValue.transform.localScale = Vector3.zero;
        textValue.gameObject.SetActive(true);
        textValue.transform.DOScale(Vector3.one, 0.6f).SetEase(ease);
    }
}