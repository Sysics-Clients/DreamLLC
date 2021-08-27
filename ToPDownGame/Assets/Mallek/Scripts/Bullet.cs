﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed=20;
    Rigidbody rb;
    public float damege;


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
    }
    IEnumerator stop(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other.transform.name);
        if (other.transform.tag == "enemy")
        {
            other.gameObject.GetComponent<EnemyBehavior>().takeDamage(damege);
        }
        else if (other.transform.tag == "Sniper")
        {
            other.gameObject.GetComponent<SniperBehavior>().takeDamage(damege);
        }
        this.gameObject.SetActive(false);
    }
}
