using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform gun;
    public PlayerBehavior playerBehavior;
    public Animator animator;
    BulletPool bulletPool;
    public float reloadPeriod;
   
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = BulletPool.Instance;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Test Shoot On Pc
        if (Input.GetKeyUp(KeyCode.Z))
        {
            if (!animator.GetBool("attack"))
            {
                animator.SetBool("attack", true);
                StartCoroutine("reload", reloadPeriod);
            }
        }
    }

    public void shoot()
    {
        if (!animator.GetBool("attack"))
        {
            animator.SetBool("attack", true);
            StartCoroutine("reload", 0.5f);
        }
        
    }
    public void shot()
    {
        bulletPool.spownBullet(gun.transform.position,transform.forward);
        
    }

    IEnumerator reload(float wait)
    {
        yield return new WaitForSeconds(wait);
        animator.SetBool("attack", false);
    }
}
