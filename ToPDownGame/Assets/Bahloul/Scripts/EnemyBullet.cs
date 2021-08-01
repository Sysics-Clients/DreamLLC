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
    Coroutine cour;
    private void Start()
    {
        enemyBehavior = sender.GetComponent<EnemyBehavior>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerBehavior>().damege((int)sender.GetComponent<EnemyBehavior>().Item.damage);
            Instantiate(BloodEffect, transform.position, Quaternion.identity);
        }
        enemyBehavior.returnBullet(gameObject);
    }
    private void OnDisable()
    {
        if (enemyBehavior != null)
        {
            enemyBehavior.returnBullet(this.gameObject);
            StopCoroutine(cour);
        }
    }
    private void OnEnable()
    {
        cour=StartCoroutine(WaitToDestroy(TimeToDestroy));
    }

    private IEnumerator WaitToDestroy(float TimeToDestroy)
    {
        yield return new WaitForSeconds(TimeToDestroy);
        if (enemyBehavior != null)
            enemyBehavior.returnBullet(gameObject);
        else
            Destroy(gameObject);
    }
}
