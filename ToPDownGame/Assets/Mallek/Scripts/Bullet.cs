using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject audioManager;
    public float speed=20;
    Rigidbody rb;
    public float damege;
    public GameObject BloodEffect;
    public GameObject MetalEffect;
    public GameObject WoodEffect;




    private void OnEnable()
    {
        rb.velocity = transform.forward * speed;
        StartCoroutine(stop(2));
    }
    private void Start()
    {
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
    }
    IEnumerator stop(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other.transform.name);
        if (other.tag == "enemy")
        {
            other.gameObject.GetComponent<EnemyBehavior>().takeDamage(damege);
            Instantiate(BloodEffect, other.transform.position, Quaternion.identity);
        }
        else if (other.tag == "Sniper")
        {
            other.gameObject.GetComponent<SniperBehavior>().takeDamage(damege);
            Instantiate(BloodEffect, other.transform.position, Quaternion.identity);
        } else if (other.tag == "Metal")
        {
            audioManager.GetComponent<AudioManager>().PlaySound(AudioManager.Sounds.Metal);
            Instantiate(MetalEffect, other.transform.position, Quaternion.identity);

        }
        else if (other.tag == "Wood")
        {
            audioManager.GetComponent<AudioManager>().PlaySound(AudioManager.Sounds.Wood);
            Instantiate(WoodEffect, other.transform.position, Quaternion.identity);

        }
        this.gameObject.SetActive(false);
    }
}
