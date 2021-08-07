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
    private SniperBehavior sniperBehavior;
    Coroutine cour;
    private void Start()
    {
        if (sender.tag == "Sniper")
        {
            sniperBehavior= sender.GetComponent<SniperBehavior>();
        }
        else if(sender.tag=="enemy")
        {
            enemyBehavior = sender.GetComponent<EnemyBehavior>();
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (sender.tag == "Sniper")
            {
                other.gameObject.GetComponent<PlayerBehavior>().damege((int)sender.GetComponent<SniperBehavior>().Item.damage);
                sniperBehavior.returnBullet(gameObject);
            }
            else if (sender.tag == "enemy")
            {
                other.gameObject.GetComponent<PlayerBehavior>().damege((int)sender.GetComponent<EnemyBehavior>().Item.damage);
                enemyBehavior.returnBullet(gameObject);
            }
            
            
            
            Instantiate(BloodEffect, transform.position, Quaternion.identity);
        }

    }
    private void OnDisable()
    {
        if(sender != null)
            if ((sender.tag == "Sniper"))
            {
                if (sniperBehavior != null)
                {
                    sniperBehavior.returnBullet(this.gameObject);
                    StopCoroutine(cour);
                }
            }
            else if (sender.tag == "enemy")
            {
                if (enemyBehavior != null)
                {
                    enemyBehavior.returnBullet(this.gameObject);
                    StopCoroutine(cour);
                }
            }
    }
    private void OnEnable()
    {
        cour=StartCoroutine(WaitToDestroy(TimeToDestroy));
    }

    private IEnumerator WaitToDestroy(float TimeToDestroy)
    {
        yield return new WaitForSeconds(TimeToDestroy);
        if (sender.tag == "Sniper")
        {
            if (sniperBehavior != null)
                sniperBehavior.returnBullet(gameObject);
            else
                Destroy(gameObject);
        }
        else if (sender.tag == "enemy")
        {
            if (enemyBehavior != null)
                enemyBehavior.returnBullet(gameObject);
            else
                Destroy(gameObject);
        }





       
    }
}
