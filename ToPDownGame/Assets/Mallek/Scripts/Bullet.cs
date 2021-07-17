using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed=20;
    Rigidbody rb;
    private void OnEnable()
    {
        rb.velocity = transform.forward * speed;
        StartCoroutine(stop());
    }
    private void Start()
    {
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    

    
    IEnumerator stop()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "enemy")
        {
            other.gameObject.GetComponent<EnemyBehavior>().takeDamage(10);
        }
        this.gameObject.SetActive(false);
    }
}
