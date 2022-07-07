using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons")]
public class WeaponItem : ItemObjects
{
    public Sprite presentation;
    public GameObject bullet;
    public RuntimeAnimatorController animator;
    public AudioClip AudioReload, emptyGun;
    public int damege;
    public int reload;
    public float wait;
    public float speed;

    private void OnValidate()
    {
        if(bullet!=null)
            bullet.GetComponent<Bullet>().damege = damege;
    }
    
}

