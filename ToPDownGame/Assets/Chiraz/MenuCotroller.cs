using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuCotroller : MonoBehaviour
{
    public GameObject PlayerInSHop;
    public GameObject ThanksInApp;
    public Text thanksText;
    public List<ShopItems> shopItems;
    public RectTransform shopMenu, changechar, weaponsmenu, coins;
    public AudioMixer soundMixer;
public GameObject Story;
public GameObject menu,panelNoMony;
    // Start is called before the first frame update
    private void OnDisable()
    {
        GeneralEvents.noMony -= monyPanel;
    }
    private void OnEnable()
    {
        GeneralEvents.noMony += monyPanel;
    }
    private void Awake()
    {
        if (PlayerPrefs.HasKey("ShowStory"))
        {
            menu.SetActive(true);
            Story.SetActive(false);
        }
        if (PlayerPrefs.HasKey("m"))
        {
            float datasound = PlayerPrefs.GetFloat("music");
            Debug.Log("We are here" + datasound);
            if (datasound <= 0)
            {
                datasound = -80;
            }
            soundMixer.SetFloat("MasterVolume", Mathf.Log10(datasound) * 20);

           
        }
        else
        {
            soundMixer.SetFloat("MasterVolume", Mathf.Log10(0.5f) * 20);
            PlayerPrefs.SetFloat("music", 0.5f);

        }

        if (!PlayerPrefs.HasKey("effect"))
        {
            
            soundMixer.SetFloat("MasterVolumeEffect", Mathf.Log10(0.5f) * 20);
            PlayerPrefs.SetFloat("effect", 0.5f);
        }
        else
        {
            float datasound = PlayerPrefs.GetFloat("effect");
           
            if (datasound <= 0)
            {
                datasound = -80;
            }
            soundMixer.SetFloat("MasterVolumeEffect", Mathf.Log10(datasound) * 20);

        }

    }
    void Start ()
    {
        PlayerPrefs.SetFloat("ShowStory",1);
    }

    public void vibration()
    {
       
    }
    // Update is called once per frame
    public void shopbtn()
    {
        shopMenu.DOAnchorPos(Vector2.zero, 0.50f);
        //StartCoroutine(ShowPlayer());
        PlayerInSHop.SetActive(true);

    }
    IEnumerator ShowPlayer()
    {
        yield return new WaitForSeconds(1);
        

    }
    IEnumerator CaroutinreLoadScene()
    {
        yield return new WaitForSeconds(5);

        if (Singleton._instance.Level == 0)
        {
            SceneManager.LoadScene(Singleton._instance.Level + 2);
        }
        else
        {
            SceneManager.LoadScene(Singleton._instance.Level);
        }
    }
    public void LoadScene()
    {
        LoadingController loadingController = GameObject.FindObjectOfType<LoadingController>();
        loadingController.init();
        StartCoroutine(CaroutinreLoadScene());
    }
    public void weaponbtn()
    {
        weaponsmenu.DOAnchorPos(Vector2.zero, 0.50f);
    }

    public void changecharbtn()
    {
        changechar.DOAnchorPos(Vector2.zero, 0.50f);
    }
    public void coinshop()
    {
        coins.DOAnchorPos(Vector2.zero, 0.50f);
    }
    public void closechangechar()
    {
        changechar.DOAnchorPos(new Vector2(2800,0), 0.50f);
    }
    public void closecoinsshop()
    {
        coins.DOAnchorPos(new Vector2(0, 2600), 0.50f);
        if (shopMenu.anchoredPosition==Vector2.zero)
        {
            PlayerInSHop.SetActive(true);
        }
    }
    public void closeshopmenu()
    {
        shopMenu.DOAnchorPos(new Vector2(0, 2800), 0.50f);
    }
    public void closeweaponsmenu()
    {
        weaponsmenu.DOAnchorPos(new Vector2(2800, 0), 0.50f);
    }

   
    public void monyPanel()
    {
        panelNoMony.SetActive(true);
    }

    public void OnCompletePurchase(int coins)
    {
        ThanksInApp.SetActive(true);
        thanksText.text = "You purchased " + coins + " coins";  
        Singleton._instance.addCoin(coins);
        Singleton._instance.save();
    }
}
