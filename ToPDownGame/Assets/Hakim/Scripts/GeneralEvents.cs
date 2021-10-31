using UnityEngine;

public class GeneralEvents
{
    public delegate void ShakeErreurMessage();
    public static ShakeErreurMessage shakeErreurMessage;

    public delegate void HideErreurMessage(float time=0);
    public static HideErreurMessage hideErreurMessage;

    public delegate void ToNewScene(string NewSceneName);
    public static ToNewScene toNewScene;

    public delegate bool TestAllCompletion(MissionName mission=MissionName.NoMissionAvailale);
    public static TestAllCompletion testAllCompletion;

    public delegate void StopEnemies();
    public static StopEnemies stopEnemies;

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

    public delegate void Health(float health, float armor);
    public static Health health;

    public delegate void PlaySound(AudioManager.Sounds sound);
    public PlaySound playSound;

    public delegate bool ChangeWeopen(ItemTypes type);
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

    /*public delegate void ChangeColorHealth();
    public static ChangeColorHealth changeColorHealth;*/

    public delegate void ChangeColorWeaponButton(Color c, int w);
    public static ChangeColorWeaponButton changeColorWeaponButton;

    public delegate void SetSpeed(float v);
    public static SetSpeed setSpeed;

    public delegate bool GetCanChange();
    public static GetCanChange getCanChange;
    
    public delegate ItemTypes GetWeaponType();
    public static GetWeaponType getWeaponType;

    public delegate void StartBullets();
    public static StartBullets startBullets;

    public delegate void OnTaskFinish(MissionName missionName, int id = 0);
    public static OnTaskFinish onTaskFinish;

    public delegate bool CheckMissionCompletion(MissionName missionName, int id = 0);
    public static CheckMissionCompletion checkMissionCompletion;

    public delegate void WriteErrorMessage(string err,Color color);
    public static WriteErrorMessage writeErrorMessage;

    public delegate void SetMissionObjectAndSprite(GameObject tr = null, Sprite sp = null);
    public static SetMissionObjectAndSprite setMissionObjectAndSprite;

    public delegate void ChangePlayerPos(PlayerBehavior.PlayerPos pos);
    public static ChangePlayerPos changePlayerPos;

    public delegate void Select();
    public static Select select;

    public delegate void ShowItem(ItemObjects item);
    public static ShowItem showItem;

    public delegate void Buy();
    public static Buy buyWeapon;
    public static Buy buyClowths;

    public delegate void SetItem(ItemObjects item);
    public static SetItem setItem;

    public delegate void ActiveItems(ItemTypes types);
    public static ActiveItems activeItems;

    public delegate void SetWeapon(WeaponItem ak, WeaponItem pistol, WeaponItem knife);
    public static SetWeapon setItems;

    public delegate void SetClwths(ItemObjects top, ItemObjects bot, ItemObjects shoos, ItemObjects casque);
    public static SetClwths setClwths;

    public delegate void StateItem();
    public static StateItem toBuyWeapon;
    public static StateItem toUseWeapon;
    public static StateItem isCurrentWeapon;

    public static StateItem toBuyClow;
    public static StateItem toUseClow;
    public static StateItem isCurrentClow;

    public delegate void UseItem();
    public static UseItem useIte;

    public delegate void BtnUseItem(ItemObjects item);
    public static BtnUseItem btnUseIte;
    /*public delegate void Situation();
    public static Situation win;
    public static Situation lose;
    */

    public delegate void SetCoin(int v);
    public static SetCoin setCoin;

    public static clothes currentClothes;
    public class clothes{
        public GameObject hat, pants, shoes, shirt, shield;
    }

    public delegate void EnemyDamage(float health,Vector3 pos);
    public static EnemyDamage enemyDamage;
} 
