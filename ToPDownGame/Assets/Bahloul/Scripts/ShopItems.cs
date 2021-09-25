using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shop item", menuName = "Shop")]
public class ShopItems : ScriptableObject
{
    public ItemType itemtype;
    public string itemName;
    public Sprite itemSprite;
    public float price;
    public bool isSelected;
    public bool isBought;

}
public enum ItemType { pants, shirt, shoes, shield, hat }