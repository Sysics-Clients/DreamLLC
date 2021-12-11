using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuCotroller : MonoBehaviour
{
    public GameObject PlayerInSHop;
    public List<ShopItems> shopItems;
    public RectTransform shopMenu, changechar, weaponsmenu, coins;
    // Start is called before the first frame update
    void Start()
    {
        
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
            SceneManager.LoadScene(Singleton._instance.Level + 1);
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

}
