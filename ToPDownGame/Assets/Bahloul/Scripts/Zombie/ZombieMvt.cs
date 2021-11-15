using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMvt : MonoBehaviour
{
    public ZombieBehavior zombieBehavior;
    private void OnEnable()
    {
        zombieBehavior.enemyMovement += changeMovement;
    }
    private void OnDisable()
    {
        zombieBehavior.enemyMovement += changeMovement;
    }
    public enum Movement
    {
        Idle,
        Walk,
        Attack,
        Die,
    }
    private Movement currentMovement = Movement.Idle;
    private Animator anim;

    public void changeMovement(Movement move)
    {
        if(anim.GetBool("Die"))
            anim.SetBool("Die", false);
        currentMovement = move;
        switch (move)
        {
            case Movement.Idle:
                anim.SetFloat("Speed", 0f);
                break;
            case Movement.Walk:
                anim.SetFloat("Speed", 0.5f);
                break;
            case Movement.Attack:
                anim.SetFloat("Speed", 1f);
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
