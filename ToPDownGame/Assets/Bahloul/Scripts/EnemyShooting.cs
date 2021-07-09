using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public short speed = 25;
    public GameObject bullet_;
    [SerializeField]
    private Transform firePoint;

    private void OnEnable()
    {
        
    }
    private void shoot()
    {
        GameObject clone = Instantiate(bullet_, firePoint.position, firePoint.rotation);
        clone.GetComponent<Rigidbody>().velocity = transform.forward * speed;
        clone.GetComponent<EnemyBullet>().sender = gameObject;
    }
    public void ShootBullet()
    {
        shoot();
    }
}
