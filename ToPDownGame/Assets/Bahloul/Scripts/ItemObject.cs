using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ShopItems item;

    public void OnClick()
    {
        GameObject go= GameObject.Find(item.itemName);
        switch (item.itemtype)
        {
            case ItemType.hat:
                GeneralEvents.currentClothes.hat.SetActive(false);
                GeneralEvents.currentClothes.hat = go;
                break;
            case ItemType.pants:
                GeneralEvents.currentClothes.pants.SetActive(false);
                GeneralEvents.currentClothes.pants = go;
                break;
            case ItemType.shield:
                GeneralEvents.currentClothes.shield.SetActive(false);
                GeneralEvents.currentClothes.shield = go;
                break;
            case ItemType.shirt:
                GeneralEvents.currentClothes.shirt.SetActive(false);
                GeneralEvents.currentClothes.shirt = go;
                break;
            case ItemType.shoes:
                GeneralEvents.currentClothes.shoes.SetActive(false);
                GeneralEvents.currentClothes.shoes = go;
                break;
        }
        go.SetActive(true);
    }
}
