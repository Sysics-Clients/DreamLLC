using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject audioManager;
    [SerializeField]
    private float TimeToDestroy;
    public GameObject sender;
    public GameObject BloodEffect;
    public GameObject MetalEffect;
    public GameObject WoodEffect;
    private EnemyBehavior enemyBehavior;
    private SniperBehavior sniperBehavior;
    Coroutine cour;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
    }
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

            if (sender==null)
            {
                return;
            }
            if (sender.tag == "Sniper")
            {
                if(other.gameObject.GetComponent<PlayerBehavior>().damege!=null)
                other.gameObject.GetComponent<PlayerBehavior>().damege((int)sender.GetComponent<SniperBehavior>().Item.damage);
            }
            else if (sender.tag == "enemy")
            {
                if(other.gameObject.GetComponent<PlayerBehavior>().damege!=null)
                other.gameObject.GetComponent<PlayerBehavior>().damege((int)sender.GetComponent<EnemyBehavior>().Item.damage);
            }
            Instantiate(BloodEffect, other.transform.position, Quaternion.identity);
        }
        else if (other.tag == "Metal")
        {
            audioManager.GetComponent<AudioManager>().PlaySound(AudioManager.Sounds.Metal);
            Instantiate(MetalEffect, other.transform.position, Quaternion.identity);

        }
        else if (other.tag == "Wood")
        {
            audioManager.GetComponent<AudioManager>().PlaySound(AudioManager.Sounds.Wood);
            Instantiate(WoodEffect, other.transform.position, Quaternion.identity);

        }

        if (sender == null)
        {
            return;
        }
        if (sender.tag == "Sniper")
        {
            sniperBehavior.returnBullet(gameObject);
        }
        else if (sender.tag == "enemy")
        {
            enemyBehavior.returnBullet(gameObject);
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
        if(sender!=null)
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
        else
                Destroy(gameObject);






    }
}
