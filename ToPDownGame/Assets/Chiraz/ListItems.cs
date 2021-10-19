using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List Item", menuName = "Inventory System/Items/list item")]
public class ListItems : ScriptableObject
{
    public  List<ItemObjects> items;
}
