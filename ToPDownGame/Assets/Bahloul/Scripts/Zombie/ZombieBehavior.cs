using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    public GameObject player;
    public bool isVisible = true;
    
    public Canvas EnemyCanvas;
    public float TimeToDisappear = 3;

    public delegate void EnemyMovement(ZombieMvt.Movement move);
    public EnemyMovement enemyMovement;

    public delegate void EnemyState(EnemyStates.State state);
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
    private void dying()
    {
        StartCoroutine(WaitAndDie());
    }
    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(TimeToDisappear);
        Destroy(gameObject);
    }
}
