using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShooting : MonoBehaviour
{
    public SniperBehavior sniperBehavior;
     Animator anim;
    public short Bulletspeed = 25;
    public short GrenadeSpeed = 200;
    [SerializeField]
    private Transform GrenadeFirePoint;
    [SerializeField]
    private Transform BulletFirePoint;
    [SerializeField]
    float FireRateBullet;
    public GameObject Grenade;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ThrowGrenade() {
        sniperBehavior.enemyMovement(SniperMovement.Movement.Idle);
        Vector3 vo = Calculatevelocity(sniperBehavior.playerTransform.position, GrenadeFirePoint.position, GrenadeSpeed);
        shootGrenade(vo);
        StartCoroutine(WaitToShoot());
    }
    void shootGrenade(Vector3 vo)
    {
        GameObject go = Instantiate(Grenade, GrenadeFirePoint.position, GrenadeFirePoint.rotation);
        go.GetComponent<Rigidbody>().velocity = vo;
        go.GetComponent<GrenadeEffect>().sender = gameObject;
    }
    Vector3 Calculatevelocity(Vector3 target, Vector3 origin, float time)
{
    Vector3 distance = target - origin;
    Vector3 distanceXZ = distance;
    distanceXZ.y = 0f;

    float Sy = distance.y;
    float sXZ = distanceXZ.magnitude;

    float Vxz = sXZ / time;
    float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

    Vector3 result = distanceXZ.normalized;
    result *= Vxz;
    result.y = Vy;

    return result;

}

    IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("isShooting", true);
        sniperBehavior.changeGun(2);
        StartCoroutine(WaitToThrowGrenade());
    }
    IEnumerator WaitToThrowGrenade()
    {
        yield return new WaitForSeconds(10);
        anim.SetBool("isShooting", false);
        if (Vector3.Distance(sniperBehavior.playerTransform.position, transform.position) < 50)
        {
            sniperBehavior.enemyMovement(SniperMovement.Movement.ThrowGrenade);
        }
        else
        {
            sniperBehavior.toIdle();
        }
    }
    
    private void shoot()
{
    GameObject newBullet = sniperBehavior.getBullet();
    newBullet.transform.position = BulletFirePoint.position;
    newBullet.transform.rotation = BulletFirePoint.rotation;
        //newBullet.GetComponent<Rigidbody>().velocity = BulletFirePoint.forward*Bulletspeed;
        newBullet.GetComponent<Rigidbody>().velocity = (new Vector3(sniperBehavior.playerTransform.position.x,sniperBehavior.playerTransform.position.y-0.5f, sniperBehavior.playerTransform.position.z) - transform.position).normalized * Bulletspeed;
    newBullet.GetComponent<EnemyBullet>().sender = gameObject;
}


public void ShootBullet()
    {
        shoot();
    }
}
