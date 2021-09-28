using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons")]
public class WeaponItem : ScriptableObject
{
    public string nameWeap;
    public string desc;
    public GameObject Prefab;
    public GameObject bullet;
    public RuntimeAnimatorController animator;
    public int reload;
    public float wait;
    public WeopenType weopenType;
    public AudioClip AudioReload, emptyGun;
    public float speed;


}
public enum WeopenType { AK, Gun }