using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class FinishGame : MonoBehaviour
{
    public GameStatePanel gameStatePanel;
    public GameObject loadingScene;
    public Text coinswin;
    public Text experience;
    int coinswinValue;
    bool isVideoRewarded;
    private void OnEnable()
    {
        if (gameStatePanel!=null)
        {
            StartCoroutine(startAnimation());
        }
        EventController.videoRewarded += AdsRewardState;
    }
    private void OnDisable()
    {
        EventController.videoRewarded -= AdsRewardState;

    }
    public void ShowReward()
    {
        AdsManager._instance.ShowRewardVideo("DefaultRewardedVideo");
        AdsRewardState(true);
    }
    void AdsRewardState(bool check)
    {
        if (check == true)
        {
            isVideoRewarded = true;
            if (coinswin != null)
            {
                coinswinValue = coinswinValue * 3;
                loadScene(true);
            }
            else
            {
                loadScene(false);
            }
        }
    }
    IEnumerator startAnimation()
    {
        yield return new WaitForFixedUpdate();
        if (coinswin!=null)
        {
            coinswinValue = Random.Range(3, 8);
            coinswin.text = "+" + coinswinValue;
            experience.text = "+" + Random.Range(10, 30);
        }
        else
        {
            coinswinValue = 0;
        }

      
       
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
        if (isVideoRewarded==false&&iswin==true)
        {
            if (AdsManager._instance.verifInter())
            {
                AdsManager._instance.ShowIntertiate("DefaultInterstitial");
            }
        }
        
        Singleton._instance.addCoin(coinswinValue);
        Singleton._instance.save();
        loadingScene.SetActive(true);
        loadingScene.GetComponent<LoadingScreen>().isWin = iswin;
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

        if (collectButton!=null)
        {
            RectTransform collectrect = collectButton.GetComponent<RectTransform>();
            collectrect.position = new Vector3(collectrect.position.x + 60, adsrect.position.y, adsrect.position.z);
            collectButton.image.DOFade(0, 0.0f);
            collectButton.gameObject.SetActive(true);
            collectrect.DOMoveX(collectrect.position.x - 60, 1).SetEase(easeBG);
            collectButton.image.DOFade(1, 0.3f);
        }
       


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