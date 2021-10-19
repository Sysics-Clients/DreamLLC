using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentItem", menuName = "Current")]
public class CurrentItem : ScriptableObject
{
    public WeaponItem ak, pistol, knife;
    public ItemObjects  top, bot, shoos, shield, casque;
}
