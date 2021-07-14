using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public short speed = 25;
    [SerializeField]
    private Transform firePoint;
    public EnemyBehavior enemyBehavior;
    private void OnEnable()
    {
        
    }
    private void shoot()
    {
        //GameObject clone = Instantiate(bullet_, firePoint.position, firePoint.rotation);
        GameObject newBullet = enemyBehavior.getBullet();
        newBullet.transform.position = firePoint.position;
        newBullet.GetComponent<Rigidbody>().velocity = transform.forward * speed;
        newBullet.GetComponent<EnemyBullet>().sender = gameObject;
    }
    public void ShootBullet()
    {
        shoot();
    }
}
