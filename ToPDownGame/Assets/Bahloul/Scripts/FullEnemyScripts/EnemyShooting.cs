using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public short speed = 25;
    [SerializeField]
    private Transform firePoint;
    public EnemyBehavior enemyBehavior;
    private float gunBloom;
    public void changeBloom(float _gunBloom)
    {
        gunBloom = _gunBloom;
    }
    private void OnEnable()
    {
        enemyBehavior.changeGunBloom += changeBloom;
    }
    private void OnDisable()
    {
        enemyBehavior.changeGunBloom -= changeBloom;
    }
    private void Start()
    {
        gunBloom = enemyBehavior.gunBloom;
    }
    private void shoot()
    {
        if (enemyBehavior.canSeeThePlayer())
            gunBloom = enemyBehavior.gunBloom;
        else
            gunBloom = Random.Range(-2f, 2f);
        //GameObject clone = Instantiate(bullet_, firePoint.position, firePoint.rotation);
        GameObject newBullet = enemyBehavior.getBullet();
        newBullet.transform.position = firePoint.position;
        newBullet.GetComponent<Rigidbody>().velocity = transform.forward * speed+transform.up*gunBloom+transform.right*gunBloom;
        newBullet.GetComponent<EnemyBullet>().sender = gameObject;
    }
    public void ShootBullet()
    {
        if (!enemyBehavior.isVisible)
            return;
        shoot();
    }
}
