using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour
{

    public static AdsManager _instance;
    public string AdsState;
    public string key;


    // Start is called before the first frame update
    void Start()
    {


       // IronSource.Agent.setUserId(AppsFlyer.getAppsFlyerId());
        // AppsFlyer.validateReceipt();
        IronSource.Agent.shouldTrackNetworkState(true);
#if UNITY_ANDROID
        string appKey = "10d69db1d"; 
#elif UNITY_IPHONE
        string appKey = "8545d445";
#else
        string appKey = "unexpected_platform";
#endif



        IronSource.Agent.init(appKey);
        IronSource.Agent.validateIntegration();

    }
    private void Awake()
    {
        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            //Rest of your Awake code

        }
        else
        {

            Destroy(gameObject);
        }
        //StartCoroutine(LoadInter());



    }

    private void OnEnable()
    {
        InitRewardVideo();
        InitIntertiate();
        InitBanner();

    }

    private void OnDisable()
    {
        DiInitRewardVideo();
        DeIntertiate();
        DeInitBanner();
    }


    #region rewardVideo 
    void InitRewardVideo()
    {
        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
        IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;
    }
    void DiInitRewardVideo()
    {
        IronSourceEvents.onRewardedVideoAdOpenedEvent -= RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent -= RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent -= RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent -= RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent -= RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent -= RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent -= RewardedVideoAdShowFailedEvent;
        IronSourceEvents.onRewardedVideoAdClickedEvent -= RewardedVideoAdClickedEvent;

    }

    void RewardedVideoAvailabilityChangedEvent(bool canShowAd)
    {
        //when canShowAd false we will disable all button was request a Reward Ads

        AdsState = "unity-script: I got RewardedVideoAvailabilityChangedEvent, value = " + canShowAd;
       /* if (EventController.chnageButtonRewardRequest != null)
        {
            EventController.chnageButtonRewardRequest(canShowAd);
        }*/

        Debug.Log("unity-script: I got RewardedVideoAvailabilityChangedEvent, value = " + canShowAd);
    }

    void RewardedVideoAdOpenedEvent()
    {

        AdsState = "unity-script: I got RewardedVideoAdOpenedEvent";
        Debug.Log("unity-script: I got RewardedVideoAdOpenedEvent");

    }

    void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
    {
       /* if (EventController.videoRewarded != null)
        {
            EventController.videoRewarded(true);
        }*/
        AdsState = "unity-script: I got RewardedVideoAdRewardedEvent, amount = " + ssp.getRewardAmount() + " name = " + ssp.getRewardName();
        Debug.Log("unity-script: I got RewardedVideoAdRewardedEvent, amount = " + ssp.getRewardAmount() + " name = " + ssp.getRewardName());

    }

    void RewardedVideoAdClosedEvent()
    {
        /*if (EventController.videoRewarded != null)
        {
            EventController.videoRewarded(false);
        }*/
        AdsState = "unity-script: I got RewardedVideoAdClosedEvent";
        Debug.Log("unity-script: I got RewardedVideoAdClosedEvent");
    }

    void RewardedVideoAdStartedEvent()
    {
        AdsState = "unity-script: I got RewardedVideoAdStartedEvent";
        Debug.Log("unity-script: I got RewardedVideoAdStartedEvent");
    }

    void RewardedVideoAdEndedEvent()
    {
        AdsState = "unity-script: I got RewardedVideoAdEndedEvent";
        Debug.Log("unity-script: I got RewardedVideoAdEndedEvent");
    }

    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
        /*if (EventController.videoRewarded != null)
        {
            EventController.videoRewarded(false);
        }
        if (EventController.chnageButtonRewardRequest != null)
        {
            EventController.chnageButtonRewardRequest(false);
        }*/
        AdsState = "unity-script: I got RewardedVideoAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription();
        Debug.Log("unity-script: I got RewardedVideoAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void RewardedVideoAdClickedEvent(IronSourcePlacement ssp)
    {
        AdsState = "unity-script: I got RewardedVideoAdClickedEvent, name = " + ssp.getRewardName();
        Debug.Log("unity-script: I got RewardedVideoAdClickedEvent, name = " + ssp.getRewardName());
    }

    public void ShowRewardVideo(string s)
    {

        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo(s);
        }

    }

    public bool VerifRewarded()
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            return true;
        }
        else
        {
            IronSource.Agent.loadInterstitial();
            return false;
        }
    }

    public bool verifInter()
    {
        if (IronSource.Agent.isInterstitialReady())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GoToNextScene(string name)
    {
        StartCoroutine(LoadYourAsyncScene(name));
    }

    IEnumerator LoadYourAsyncScene(string name)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    #endregion
    #region Intertiate
    void InitIntertiate()
    {
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
    }

    void DeIntertiate()
    {
        IronSourceEvents.onInterstitialAdReadyEvent -= InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent -= InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent -= InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent -= InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent -= InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent -= InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent -= InterstitialAdClosedEvent;
    }
    void InterstitialAdReadyEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdReadyEvent");
    }

    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
        Debug.Log("unity-script: I got InterstitialAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
    }

    void InterstitialAdShowSucceededEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdShowSucceededEvent");

    }

    void InterstitialAdShowFailedEvent(IronSourceError error)
    {
        Debug.Log("unity-script: I got InterstitialAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void InterstitialAdClickedEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdClickedEvent");
    }

    void InterstitialAdOpenedEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdOpenedEvent");
    }

    void InterstitialAdClosedEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdClosedEvent");
    }

    public void ShowIntertiate(string s)
    {
        if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial(s);
        }
        else
        {
            IronSource.Agent.loadInterstitial();
            Debug.Log("unity-script: IronSource.Agent.isInterstitialReady - False");
        }
    }
    #endregion

    #region Banner
    void InitBanner()
    {
        // Add Banner Events
        IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
        IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
        IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
        IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
        IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
        IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;

        //Add ImpressionSuccess Event
        IronSourceEvents.onImpressionSuccessEvent += ImpressionSuccessEvent;
    }
    void DeInitBanner()
    {
        // Add Banner Events
        IronSourceEvents.onBannerAdLoadedEvent -= BannerAdLoadedEvent;
        IronSourceEvents.onBannerAdLoadFailedEvent -= BannerAdLoadFailedEvent;
        IronSourceEvents.onBannerAdClickedEvent -= BannerAdClickedEvent;
        IronSourceEvents.onBannerAdScreenPresentedEvent -= BannerAdScreenPresentedEvent;
        IronSourceEvents.onBannerAdScreenDismissedEvent -= BannerAdScreenDismissedEvent;
        IronSourceEvents.onBannerAdLeftApplicationEvent -= BannerAdLeftApplicationEvent;

        //Add ImpressionSuccess Event
        IronSourceEvents.onImpressionSuccessEvent -= ImpressionSuccessEvent;
    }

    void BannerAdLoadedEvent()
    {
        Debug.Log("unity-script: I got BannerAdLoadedEvent");
    }

    void BannerAdLoadFailedEvent(IronSourceError error)
    {
        Debug.Log("unity-script: I got BannerAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
    }

    void BannerAdClickedEvent()
    {
        Debug.Log("unity-script: I got BannerAdClickedEvent");

    }

    void BannerAdScreenPresentedEvent()
    {
        Debug.Log("unity-script: I got BannerAdScreenPresentedEvent");
    }

    void BannerAdScreenDismissedEvent()
    {
        Debug.Log("unity-script: I got BannerAdScreenDismissedEvent");
    }

    void BannerAdLeftApplicationEvent()
    {
        Debug.Log("unity-script: I got BannerAdLeftApplicationEvent");
    }
    void ImpressionSuccessEvent(IronSourceImpressionData impressionData)
    {
        Debug.Log("unity - script: I got ImpressionSuccessEvent ToString(): " + impressionData.ToString());
        Debug.Log("unity - script: I got ImpressionSuccessEvent allData: " + impressionData.allData);
    }

    public void ShowBanner()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM, "Banner_bottom");
        IronSource.Agent.displayBanner();
    }

    public void DestroyBanner()
    {
        IronSource.Agent.destroyBanner();
    }
    #endregion


    IEnumerator LoadInter()
    {

        yield return new WaitForSeconds(1);
        print("that's so good");
        IronSource.Agent.loadInterstitial();
    }

}
