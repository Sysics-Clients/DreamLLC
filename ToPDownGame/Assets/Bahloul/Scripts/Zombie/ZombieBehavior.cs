using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehavior : MonoBehaviour
{
    public List<float> Damages;
    public List<float> Healths;
    public float CurrentDamage;
    public float CurrentHealth;
    public float CurrentShield;

    public BoxCollider HandCollider; 
    public GameObject player;
    public bool isVisible = true;
    
    public GameObject EnemyCanvas;
    public float TimeToDisappear = 3;

    public delegate void EnemyMovement(ZombieMvt.Movement move);
    public EnemyMovement enemyMovement;

    public delegate void EnemyState(ZombieState.State state);
    public EnemyState enemyState;

    public delegate EnemyStates.State GetCurrentState();
    public GetCurrentState getCurrentState;


    public delegate float GetCurrentHealth();
    public GetCurrentHealth currentHealth;

    public delegate void TakeDamage(float damage);
    public TakeDamage takeDamage;

    public EnemyItem Item;
    public List<SkinnedMeshRenderer> skinnedMeshRenderers;
     Animator thisAnimator;

    private void Start()
    {
        thisAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    
    private void Update()
    {

        if (isVisible == true && !thisAnimator.enabled)
        {
            thisAnimator.enabled = true;
            foreach (var item in skinnedMeshRenderers)
            {
                item.enabled = true;
            }
        }
        if (isVisible == false && thisAnimator.enabled)
        {
            thisAnimator.enabled = false;
            foreach (var item in skinnedMeshRenderers)
            {
                item.enabled = false;
            }
        }
    }
    public void enableCanvas()
    {
        EnemyCanvas.SetActive(true);
    }
    public void disableCanvas()
    {
        EnemyCanvas.SetActive(false);
    }
    public void HandColliderEnable()
    {
        HandCollider.enabled = true;
    }
    public void HandColliderDisable()
    {
        HandCollider.enabled = false;
    }
}
