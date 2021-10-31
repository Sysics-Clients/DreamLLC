using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
 
  private void Awake() {
   if (instance != null) {
     Destroy(gameObject);
   }else{
     instance = this;
     DontDestroyOnLoad(gameObject);
   }
 }
 
    public GeneralEvents generalEvents;
    public AudioSource MetalHit;
    public AudioSource WoodHit;
    public AudioSource BoxDestruction;
    public AudioSource EnemyDie;
    public AudioSource DroneHitGround;
    public enum Sounds
    {
        Metal,
        Wood,
        BoxDestruction,
        enemyDie,
        droneHitGround,
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
    
    
}
