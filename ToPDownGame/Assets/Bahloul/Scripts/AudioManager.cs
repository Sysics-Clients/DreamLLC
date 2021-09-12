﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GeneralEvents generalEvents;

    public enum Sounds
    {
        Metal,
        Wood,
        BoxDestruction,
        enemyDie,
        droneHitGround,
    }
    public AudioSource MetalHit;
    public AudioSource WoodHit;
    public AudioSource BoxDestruction;
    public AudioSource EnemyDie;
    public AudioSource DroneHitGround;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void PlaySound(Sounds s)
    {
        switch (s)
        {
            case Sounds.Metal:
                MetalHit.Play();
                break;
            case Sounds.Wood:
                WoodHit.Play();
                break;
            case Sounds.BoxDestruction:
                BoxDestruction.Play();
                break;
            case Sounds.enemyDie:
                EnemyDie.Play();
                break;
            case Sounds.droneHitGround:
                DroneHitGround.Play();
                break;
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}