using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyBehavior enemyBehavior;
    private void OnEnable()
    {
        enemyBehavior.enemyMovement += changeMovement;
    }
    private void OnDisable()
    {
        enemyBehavior.enemyMovement -= changeMovement;
    }
    public enum Movement
    {
        Idle,
        Walk,
        Run,
        Crouch,
        Die,
    }
    private Movement currentMovement = Movement.Idle;
    private Animator anim;

    public void changeMovement(Movement move) {
        
        currentMovement = move;
        if (move != Movement.Crouch && anim.GetBool("isCrouching") == true)
            anim.SetBool("isCrouching", false);
        switch (move)
        {

            case Movement.Idle:
                anim.SetFloat("Speed", 0f);
                break;
            case Movement.Walk:
                anim.SetFloat("Speed", 0.5f);
                break;
            case Movement.Run:
                anim.SetFloat("Speed", 1f);
                break;
            case Movement.Crouch:
                anim.SetBool("isCrouching",true);
                break;
            case Movement.Die:
                anim.SetBool("Die", true);
                break;
                
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("Speed", 0f);
    }

  
}
