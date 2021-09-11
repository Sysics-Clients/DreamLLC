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


    public GameObject vfxBlood;
    bool target;
    


    private void OnEnable()
    {
        target = false;
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
        print(other.transform.name);
        if(!target)
            if (other.transform.tag == "enemy")
            {
                target = true;
                other.gameObject.GetComponent<EnemyBehavior>().takeDamage(damege);
                StopAllCoroutines();
                StartCoroutine(damegeEnemy(other.transform.position));
                return;
            }
            else if (other.transform.tag == "Sniper")
            {
                target = true;
                other.gameObject.GetComponent<SniperBehavior>().takeDamage(damege);
                StopAllCoroutines();
                StartCoroutine(damegeEnemy(other.transform.position));
                return;
            } else if (other.transform.tag == "Metal")
            {
                audioManager.GetComponent<AudioManager>().PlaySound(AudioManager.Sounds.Metal);
            }else if (other.transform.tag == "Wood")
            {
                audioManager.GetComponent<AudioManager>().PlaySound(AudioManager.Sounds.Wood);
            }
            this.gameObject.SetActive(false);
    }
    IEnumerator damegeEnemy(Vector3 v)
    {
        rb.velocity = Vector3.zero;
        GameObject g = Instantiate(vfxBlood, v,transform.rotation);
        yield return new WaitForSeconds(.2f);
        Destroy(g);
        this.gameObject.SetActive(false);
    }
}
