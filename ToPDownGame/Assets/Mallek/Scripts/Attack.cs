using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform gun, bulletStart;
    public Transform ReloadShootTransform;
    public Transform ReloadAkPos;
    public PlayerBehavior playerBehavior;
    public Animator animator;
    BulletPool bulletPool;
    public int[] nbBullet;
    public WeaponItem[] weapon;
    GameObject[] weap = new GameObject[3];
    int nbWeap;
    Coroutine coroutineShoot;
    bool onShot;
    public Transform AkshootPos;
       
    // Start is called before the first frame update
    void Start()
    {
        onShot = false;
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = weapon[0].animator;
        //weap[0]= Instantiate(weapon[0].Prefab,gun);
        weap[0] = Instantiate(weapon[0].Prefab, ReloadAkPos);
        //ReloadAkPos.gameObject.SetActive(false);
        weap[1] = Instantiate(weapon[1].Prefab, ReloadShootTransform);
        weap[1].SetActive(false);
        weap[2] = Instantiate(weapon[2].Prefab, ReloadShootTransform);
        weap[2].SetActive(false);
        //Instantiate(gun.gameObject, ReloadShootTransform);
        bulletPool = BulletPool.Instance;
        bulletPool.objectToPool = weapon[0].bullet;
        bulletPool.objectToPoolPistol= weapon[1].bullet;
        bulletPool.start();
        bulletStart = weap[0].transform.Find("pos");
        //bulletStart = AkshootPos;
        nbBullet[0] = weapon[0].reload;
        nbBullet[1] = weapon[1].reload;
        nbWeap = 0;
        //SwitchStateGun();
    }
    public void SwitchStateGun()
    {
        if (animator.GetBool("reload"))
        {
            //hakim enable gun 
            gun.gameObject.SetActive(false);
            ReloadShootTransform.gameObject.SetActive(true);
        }
        else
        {
            ReloadShootTransform.gameObject.SetActive(false);
            gun.gameObject.SetActive(true);
        }
    }

    public void OnenableReloadAk()
    {
        /*if (animator.runtimeAnimatorController!= weapon[0].animator)
        {
            return;
        }
        gun.gameObject.SetActive(false);
        ReloadAkPos.gameObject.SetActive(true);*/
    }
    public void onDisableReloadAk()
    {
        /*if (animator.runtimeAnimatorController != weapon[0].animator)
        {
            return;
        }
        gun.gameObject.SetActive(true);
        ReloadAkPos.gameObject.SetActive(false);*/
    }
    private void OnEnable()
    {
        GeneralEvents.sendShooting += shoot;
        GeneralEvents.changeWeopen += SwitchWeopen;
        playerBehavior.die += die;
    }
    private void OnDisable()
    {
        GeneralEvents.sendShooting -= shoot;
        GeneralEvents.changeWeopen -= SwitchWeopen;

        playerBehavior.die -= die;
    }
    // Update is called once per frame
    void Update()
    {
        //Test Shoot On Pc
       /* if (Input.GetMouseButtonUp(1))
        {
            shoot();
        }*/
        if (Input.GetKeyUp(KeyCode.E))
        {
            nextWeapon();
        }
    }
    //Getting From GenralEvents
    public void shoot(Vector3 sh)
    {
        //print(sh);
        if (sh == Vector3.zero)
        {
            if (nbWeap == 0)
            {
                if (coroutineShoot != null)
                    StopCoroutine(coroutineShoot);
                animator.SetBool("attack", false);
                return;
            }
            else if (nbWeap == 1&&onShot)
            {
               
                bulletPool.spownBulletPistol(bulletStart.position, transform.forward);
                //StartCoroutine("waitBullet", weapon[nbWeap].wait);
                nbBullet[1]--;
                onShot = false;
            }
            animator.SetBool("attack", false);
        }else if(sh.magnitude<0.3){
            onShot = false;
        }
        else 
        {
            onShot = true;
        }
        if (!animator.GetBool("attack")&&!(playerBehavior.getState()==MovmentControler.State.roll)&&sh != Vector3.zero)
        {
            animator.SetBool("attack", true);
        }
    }
    public void shot()
    {
        if (nbWeap==0)
        {
            if (nbBullet[0] == 0)
            {
                animator.SetBool("reload", true);
                StartCoroutine("reload", 0);
            }
            else
            {
                
                bulletPool.spownBullet(bulletStart.position, transform.forward);
                nbBullet[0]--;
                StartCoroutine("waitBullet", weapon[nbWeap].wait);
            } 
        }
        else if (nbWeap==1)
        {
            if (nbBullet[1] == 0)
            {
                animator.SetBool("reload", true);
                StartCoroutine("reload", 1);
            }
            else
            {
                //bulletPool.spownBulletPistol(bulletStart.position, transform.forward);
                //nbBullet[1]--;
                //StartCoroutine("waitBullet", weapon[nbWeap].wait);
            }
        }
    }
    IEnumerator waitBullet(float wait)
    {   
        yield return new WaitForSeconds(wait);
        //Time.timeScale = 0;
        if (animator.GetBool("attack")&&nbWeap==0)
            shot();
        //Debug.Break();
        // coroutineShoot= StartCoroutine("waitBullet", weapon[nbWeap].wait);
    }
    public void nextWeapon() {
        weap[nbWeap].SetActive(false);
        if (nbWeap == 2)
        {
            nbWeap = 0;
        }
        else
        {
            nbWeap++;
        }
        weap[nbWeap].SetActive(true);
        bool crouch = animator.GetBool("crouch");
        animator.runtimeAnimatorController = weapon[nbWeap].animator;
        animator.SetBool("crouch", crouch);
        bulletStart = weap[nbWeap].transform.Find("pos");
    }
    public void SwitchWeopen(WeopenType type)
    {
        weap[nbWeap].SetActive(false);
        switch (type)
        {
            case WeopenType.AK:
                nbWeap = 0;
                break;
            case WeopenType.Gun:
                nbWeap = 1;
                break;
            default:
                break;
        }
        if (animator.runtimeAnimatorController== weapon[nbWeap].animator)
        {
            //reload
            weap[nbWeap].SetActive(true);
            animator.SetBool("reload", true);
            StartCoroutine(reload(nbWeap));
        }
        else
        {
            weap[nbWeap].SetActive(true);
            bool crouch = animator.GetBool("crouch");
            animator.runtimeAnimatorController = weapon[nbWeap].animator;
            animator.SetBool("crouch", crouch);
            bulletStart = weap[nbWeap].transform.Find("pos");
        }
    }
    IEnumerator reload(int wap)
    {
        
        yield return new WaitForSeconds(0.5f);
      //  SwitchStateGun();
        animator.SetBool("reload", false);
        if(wap==nbWeap)
            nbBullet[wap] = weapon[wap].reload;
    }
    public void die()
    {
        this.enabled = false;   
    }
}


