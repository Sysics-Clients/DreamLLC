using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperMovement : MonoBehaviour
{
    public SniperBehavior sniperBehavior;
    private void OnEnable()
    {
        sniperBehavior.enemyMovement += changeMovement;
    }
    private void OnDisable()
    {
        sniperBehavior.enemyMovement -= changeMovement;
    }
    public enum Movement
    {
        Idle,
        Walk,
        ThrowGrenade,
        Die,
    }
    private Movement currentMovement = Movement.Idle;
    private Animator anim;

    public void changeMovement(Movement move)
    {
        if(anim.GetBool("ThrowGrenade")==true)
            anim.SetBool("ThrowGrenade", false);
        currentMovement = move;
        switch (move)
        {
            case Movement.Idle:
                sniperBehavior.changeGun(0);
                anim.SetFloat("Speed", 0f);
                break;
            case Movement.Walk:
                sniperBehavior.changeGun(1);
                anim.SetFloat("Speed", 1f);
                break;
            case Movement.ThrowGrenade:
                if((sniperBehavior!=null)&&(!sniperBehavior.isVisible))
                sniperBehavior.changeGun(-1);
                anim.SetBool("ThrowGrenade", true);
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
