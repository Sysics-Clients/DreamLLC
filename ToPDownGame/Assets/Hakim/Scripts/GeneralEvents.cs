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

    public delegate void PlaySound(AudioManager.Sounds sound);
    public PlaySound playSound;

    public delegate void ChangeWeopen(WeopenType type);
    public static ChangeWeopen changeWeopen;

    public delegate Vector2 NbBullet();
    public static NbBullet nbBullet;
    public static NbBullet nbBulletStart;

    public delegate void CloseObject(GameObject obj);
    public static CloseObject closeObject;
    public delegate void OpenObject(GameObject obj);
    public static OpenObject openObject;

    public delegate void TakeDamege();
    public static TakeDamege takeDamege;

    public delegate void ChangeColorHealth();
    public static ChangeColorHealth changeColorHealth;

    public delegate void ChangeColorWeaponButton(Color c,int w);
    public static ChangeColorWeaponButton changeColorWeaponButton;
}
