using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform gun, bulletStart;
    public PlayerBehavior playerBehavior;
    public Animator animator;
    BulletPool bulletPool;
    public int[] nbBullet;
    public WeaponItem[] weapon;
    GameObject[] weap = new GameObject[3];
    int nbWeap;
    Coroutine coroutineShoot;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = weapon[0].animator;
        weap[0]= Instantiate(weapon[0].Prefab,gun);
        weap[1] = Instantiate(weapon[1].Prefab, gun);
        weap[1].SetActive(false);
        weap[2] = Instantiate(weapon[2].Prefab, gun);
        weap[2].SetActive(false);
        bulletPool = BulletPool.Instance;
        bulletPool.objectToPool = weapon[0].bullet;
        bulletPool.objectToPoolPistol= weapon[1].bullet;
        bulletPool.start();
        bulletStart = weap[0].transform.Find("pos");
        nbBullet[0] = weapon[0].reload;
        nbBullet[1] = weapon[1].reload;
        nbWeap = 0;
    }
    private void OnEnable()
    {
        GeneralEvents.sendShooting += shoot;
        playerBehavior.die += die;
    }
    private void OnDisable()
    {
        GeneralEvents.sendShooting -= shoot;
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
        if (sh==Vector3.zero)
        {
            if(coroutineShoot!=null)
                StopCoroutine(coroutineShoot);
            animator.SetBool("attack", false);
            return;
        }
        if (!animator.GetBool("attack")&&!(playerBehavior.getState()==MovmentControler.State.roll))
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
                bulletPool.spownBulletPistol(bulletStart.position, transform.forward);
                nbBullet[1]--;
                StartCoroutine("waitBullet", weapon[nbWeap].wait);
            }
        }
    }
    IEnumerator waitBullet(float wait)
    {   
        yield return new WaitForSeconds(wait);
        if(animator.GetBool("attack"))
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

    IEnumerator reload(int wap)
    {
        yield return new WaitForSeconds(4);
        animator.SetBool("reload", false);
        if(wap==nbWeap)
            nbBullet[wap] = weapon[wap].reload;
    }
    public void die()
    {
        this.enabled = false;   
    }
}
