using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons")]
public class WeaponItem : ItemObjects
{
    public GameObject bullet;
    public RuntimeAnimatorController animator;
    public int reload;
    public float wait;
    public AudioClip AudioReload, emptyGun;
    public float speed;


}
