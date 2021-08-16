using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEvents 
{
    public delegate void AddCounter();
    public static AddCounter addCounter;

    public delegate void EnnemyDown(string nameEnemy);
    public static EnnemyDown ennemyDown;

    //Send Joystic Mvt
    public delegate void SendMvt(Vector3 vector);
    public static SendMvt sendMvt;

    //Send Joystics Shooting
    public delegate void SendShooting(Vector3 vector);
    public static SendShooting sendShooting;

    //Send Roll
    public delegate void SendRoll();
    public static SendRoll sendRoll;

    public delegate void ChangeGun();
    public static ChangeGun changeGun;

    public delegate void Health(float health,float armor);
    public static Health health;
}
