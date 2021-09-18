using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MenuCotroller : MonoBehaviour
{
    public RectTransform shopMenu, changechar, weaponsmenu, coins;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void shopbtn()
    {
        shopMenu.DOAnchorPos(Vector2.zero, 0.50f);

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
    }
    public void closeshopmenu()
    {
        shopMenu.DOAnchorPos(new Vector2(0, 2800), 0.50f);
    }

}
