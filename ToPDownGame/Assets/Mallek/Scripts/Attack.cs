using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Attack : MonoBehaviour
{
    public Transform gun, bulletStart,AkStart,pistolStart, bladeStart;
    public PlayerBehavior playerBehavior;
    public Animator animator;
    BulletPool bulletPool;
    int nbWeap;
    Coroutine coroutineShoot;
    bool onShot,canChange;
    public Transform AkshootPos;
    public AudioSource audio;
    //weapons[0] is AK ,weapons[1] is pistol
    public Weapon[] weapons;
    public GameObject vfxDeath;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(setUpWeap());
    }
    IEnumerator setUpWeap()
    {
        yield return new WaitForSeconds(.1f);
        weapons[0].nbBullet = weapons[0].weaponItem.reload;
        weapons[1].nbBullet = weapons[1].weaponItem.reload;
        onShot = false;
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = weapons[0].weaponItem.animator;
        weapons[0].weap = Instantiate(weapons[0].weaponItem.prefab, weapons[0].reloadPos);
        Instantiate(weapons[0].weaponItem.prefab, AkStart);
        Instantiate(weapons[1].weaponItem.prefab, pistolStart);
        Destroy( Instantiate(weapons[2].weaponItem.prefab, bladeStart).GetComponent<Blade>());
        weapons[0].weap.SetActive(false);
        weapons[1].weap = Instantiate(weapons[1].weaponItem.prefab, weapons[1].reloadPos);
        weapons[1].weap.SetActive(false);
        weapons[2].weap = Instantiate(weapons[2].weaponItem.prefab, weapons[2].reloadPos);
        weapons[2].weap.SetActive(false);
        bulletPool = BulletPool.Instance;
        startBullets();
        nbWeap = 0;
        if (GeneralEvents.setSpeed != null)
            GeneralEvents.setSpeed(v: weapons[0].weaponItem.speed);
        canChange = true;
        bulletStart = weapons[1].weap.transform.Find("pos");
    }
    public void startBullets()
    {
        bulletPool.objectToPool = weapons[0].weaponItem.bullet;
        bulletPool.objectToPoolPistol = weapons[1].weaponItem.bullet;
        bulletPool.start();
    }
    public void SwitchStateGun()
    {
        if (animator.GetBool("reload"))
        {
            //hakim enable gun 
            gun.gameObject.SetActive(false);
            weapons[1].reloadPos.gameObject.SetActive(true);
        }
        else
        {
            weapons[1].reloadPos.gameObject.SetActive(false);
            gun.gameObject.SetActive(true);
        }
    }
    public void OnenableReload()
    {
    }
    public void onDisableReload()
    {
        animator.SetBool("reload", false);
    }
    private void OnEnable()
    {
        GeneralEvents.setItems += setWeapons;
        GeneralEvents.nbBulletStart += getNbStartBullet;
        GeneralEvents.nbBullet += getNbBullet;
        GeneralEvents.sendShooting += shoot;
        GeneralEvents.changeWeopen += SwitchWeopen;
        GeneralEvents.getCanChange += getCanChange;
        //GeneralEvents.startBullets += startBullets;
        GeneralEvents.getWeaponType += getWeaponType;
        playerBehavior.die += die;
    }
    private void OnDisable()
    {
        GeneralEvents.setItems -= setWeapons;
        GeneralEvents.nbBulletStart -= getNbStartBullet;
        GeneralEvents.nbBullet -= getNbBullet;
        GeneralEvents.sendShooting -= shoot;
        GeneralEvents.changeWeopen -= SwitchWeopen;
        GeneralEvents.getCanChange -= getCanChange;
        //GeneralEvents.startBullets -= startBullets;
        GeneralEvents.getWeaponType -= getWeaponType;
        playerBehavior.die -= die;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            Time.timeScale = 0;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            shoot(Vector3.one);
            shot();
        }
    }
    //Getting From GenralEvents
    public void shoot(Vector3 sh)
    {
        if (weapons[nbWeap].nbBullet != 0 || weapons[nbWeap].nbTotalBullet != 0)
        {
            if (sh == Vector3.zero && !animator.GetBool("reload"))
            {
                if (coroutineShoot != null)
                {
                    StopCoroutine(coroutineShoot);
                }
                animator.SetBool("attack", false);
                return;
            }
            if (!animator.GetBool("attack") && !(playerBehavior.getState() == MovmentControler.State.roll) && sh != Vector3.zero)
            {
                animator.SetBool("attack", true);
            }
        }
        else if(sh != Vector3.zero)
        {
            if (!audio.isPlaying)
            {
                audio.clip = weapons[nbWeap].weaponItem.emptyGun;
                audio.Play();
            }
        }
    }
    public void shot()
    {
        if (nbWeap==0)
        {
            if (weapons[0]. nbBullet == 0)
            {
                if (weapons[0].nbTotalBullet!=0)
                {
                    animator.SetBool("reload", true);
                    StartCoroutine("reload", 0);
                }
            }
            else
            {
                
                bulletPool.spownBullet(weapons[0].weap.transform.position+ transform.forward, transform.forward);
                weapons[0].nbBullet--;
                StartCoroutine("waitBullet", weapons[nbWeap].weaponItem.wait);
            } 
        }else if (nbWeap == 1)
        {
            if (weapons[1].nbBullet == 0)
            {
                if (weapons[1].nbTotalBullet != 0)
                {
                    animator.SetBool("reload", true);
                    StartCoroutine("reload", 1);
                }
            }
            else
            {
                bulletPool.spownBulletPistol(bulletStart.position, transform.forward);
                weapons[1].nbBullet--;
                StartCoroutine("waitBullet", weapons[nbWeap].weaponItem.wait);
            }
        }
        if (weapons[nbWeap].nbBullet == 5)
        {
            GeneralEvents.changeColorWeaponButton(Color.red,nbWeap);
        }
    }
    IEnumerator waitBullet(float wait)
    {   
        yield return new WaitForSeconds(wait);
        if (animator.GetBool("attack"))
            shot();
    }
    public void nextWeapon() {
        weapons[nbWeap].weap.SetActive(false);
        if (nbWeap == 2)
        {
            nbWeap = 0;
        }
        else
        {
            nbWeap++;
        }
        weapons[nbWeap].weap.SetActive(true);
        bool crouch = animator.GetBool("crouch");
        animator.runtimeAnimatorController = weapons[nbWeap].weaponItem.animator;
        animator.SetBool("crouch", crouch);
    }
    public bool SwitchWeopen(ItemTypes type)
    {
        //weapons[nbWeap].weap.SetActive(false);
        if (canChange)
        {
            switch (type)
            {
                case ItemTypes.AK:
                    nbWeap = 0;
                    GeneralEvents.setSpeed(weapons[nbWeap].weaponItem.speed);
                    break;
                case ItemTypes.Pistol:

                    nbWeap = 1;
                    GeneralEvents.setSpeed(weapons[nbWeap].weaponItem.speed);
                    break;
                case ItemTypes.knife:

                    nbWeap = 2;
                    GeneralEvents.setSpeed(weapons[nbWeap].weaponItem.speed);
                    break;
                default:
                    break;
            }
            if (animator.runtimeAnimatorController == weapons[nbWeap].weaponItem.animator)
            {
                //weapons[nbWeap].weap.SetActive(true);
                animator.SetBool("reload", true);
                StartCoroutine(reload(nbWeap));
            }
            else
            {
                //weapons[nbWeap].weap.SetActive(true);
                StartCoroutine(changeGun());
                return true;
            }
        }
        return false;
    }

    IEnumerator changeGun()
    {
        canChange = false;
        animator.SetTrigger("change");
        yield return new WaitForSeconds(1f);
        bool crouch = animator.GetBool("crouch");
        animator.runtimeAnimatorController = weapons[nbWeap].weaponItem.animator;
        animator.SetBool("crouch", crouch);
    }
    IEnumerator reload(int wap)
    {
        audio.clip = weapons[nbWeap].weaponItem.AudioReload;
        audio.Play();
        yield return new WaitForSeconds(1.7f);
        audio.Stop();
        GeneralEvents.changeColorWeaponButton(Color.blue,wap);
        if (wap==nbWeap&& weapons[nbWeap]. nbTotalBullet!=0)
        {
            weapons[nbWeap].nbTotalBullet -= weapons[nbWeap].weaponItem.reload- weapons[nbWeap].nbBullet;
            weapons[nbWeap].nbBullet = weapons[nbWeap].weaponItem.reload;
            if (weapons[nbWeap].nbTotalBullet < 0)
            {
                weapons[nbWeap].nbBullet += weapons[nbWeap].nbTotalBullet;
                weapons[nbWeap].nbTotalBullet = 0;
            }
        }     
    }
    public void die()
    {
        GeneralEvents.stopEnemies();
        vfxDeath.active = true;
        audio.clip = null;
        StopAllCoroutines();
        this.enabled = false;   
    }    
    public Vector2 getNbBullet()
    {
        return new Vector2(weapons[0].nbBullet, weapons[1].nbBullet);
    }
    public Vector2 getNbStartBullet()
    {
        return new Vector2(weapons[0].nbTotalBullet, weapons[1].nbTotalBullet);
    }

    public void changeToBlade()
    {
        bladeStart.gameObject.SetActive(false);
        pistolStart.gameObject.SetActive(true);
        AkStart.gameObject.SetActive(true);
        weapons[2].weap.SetActive(true);
        weapons[0].weap.SetActive(false);
        weapons[1].weap.SetActive(false);
        canChange = true;

    }
    public void changeBlade()
    {
        bladeStart.gameObject.SetActive(true);
        weapons[2].weap.SetActive(false);
    }

    public void changeToAk()
    {
        AkStart.gameObject.SetActive(false);
        bladeStart.gameObject.SetActive(true);
        pistolStart.gameObject.SetActive(true);
        weapons[0].weap.SetActive(true);
        weapons[1].weap.SetActive(false);
        weapons[2].weap.SetActive(false);
        canChange = true;
    }
    public void changeAk()
    {
        AkStart.gameObject.SetActive(true);
        weapons[0].weap.SetActive(false);
    }
    public void changeToPistol()
    {
        pistolStart.gameObject.SetActive(false);
        bladeStart.gameObject.SetActive(true);
        AkStart.gameObject.SetActive(true);
        weapons[1].weap.SetActive(true);
        weapons[0].weap.SetActive(false);
        weapons[2].weap.SetActive(false);
        canChange = true;
    }
    public void changePistol()
    {
        pistolStart.gameObject.SetActive(true);
        weapons[1].weap.SetActive( false);
    }

    public bool getCanChange()
    {
        return canChange;
    }
    public ItemTypes getWeaponType() {
        return weapons[nbWeap].weaponItem.type;    
    }
    void setWeapons(WeaponItem ak, WeaponItem pistol, WeaponItem knife)
    {
        weapons[0].weaponItem = ak;
        weapons[1].weaponItem = pistol;
        weapons[2].weaponItem = knife;
    }
}
[System.Serializable]
public class Weapon
{
    public GameObject weap;
    public WeaponItem weaponItem;
    public Transform reloadPos;
    public int nbBullet;
    public int nbTotalBullet;
}