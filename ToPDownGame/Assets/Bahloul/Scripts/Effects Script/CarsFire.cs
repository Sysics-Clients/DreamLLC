﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsFire : MonoBehaviour
{
    public GameObject ExplosionVFX;
    private int ShotNumber=0;
    public GameObject carobj;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Bullet")&&(ShotNumber<=3))
        {
            if(ShotNumber==3)
            {
                ExplosionVFX.SetActive(true);
                ExplosionVFX.GetComponent<ParticleSystem>().Play();
                ExplosionVFX.GetComponent<AudioSource>().Play();
                carobj.SetActive(false);
                Destroy(gameObject, 1);
            }
            ShotNumber++;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
