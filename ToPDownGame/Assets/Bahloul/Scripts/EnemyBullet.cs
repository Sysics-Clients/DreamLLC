using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [SerializeField]
    private float TimeToDestroy;
    public GameObject sender;
    public GameObject BloodEffect;
    private EnemyBehavior enemyBehavior;
    private void Start()
    {
        enemyBehavior = sender.GetComponent<EnemyBehavior>();
        StartCoroutine(WaitToDestroy(TimeToDestroy));
        print("jsqfdi");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            //PlayerBehavior.instance.modifyHealth(sender.GetComponent<EnemyBehavior>().Item.damage);
            Instantiate(BloodEffect,collision.transform.position,Quaternion.Euler(transform.rotation.eulerAngles));
        }
        enemyBehavior.returnBullet(gameObject);
    }
    private void OnDisable()
    {
        if (enemyBehavior != null)
        {
            enemyBehavior.returnBullet(this.gameObject);
            
        }
    }
    private void OnEnable()
    {
        StartCoroutine(WaitToDestroy(TimeToDestroy));
    }

    private IEnumerator WaitToDestroy(float TimeToDestroy)
    {
        yield return new WaitForSeconds(TimeToDestroy);
        enemyBehavior.returnBullet(gameObject);
    }
}
