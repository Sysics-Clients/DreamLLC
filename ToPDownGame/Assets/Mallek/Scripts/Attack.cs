using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform gun;
    public PlayerBehavior playerBehavior;
    public Animator animator;
    BulletPool bulletPool;

   
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = BulletPool.Instance;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void shoot()
    {
        if(!animator.GetBool("attack"))
            animator.SetBool("attack", true);
        
    }
    public void shot()
    {
        bulletPool.spownBullet(gun.transform.position,transform.forward);
        animator.SetBool("attack", false);
    }
}
