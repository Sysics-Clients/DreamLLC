using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageController : MonoBehaviour
{
    public HostageBehavior hostageBehavior;
    private void OnEnable()
    {
        hostageBehavior.changeMovement += changeMovement;
        hostageBehavior.changeAnimationSpeed += changeAnimationSpeed;
    }
    private void OnDisable()
    {
        hostageBehavior.changeMovement -= changeMovement;
        hostageBehavior.changeAnimationSpeed -= changeAnimationSpeed;
    }
    public void changeAnimationSpeed(float s)
    {
        anim.speed = s;
    }
    public enum Movement
    {
        sleep,
        standUp,
        Walk,
        idle,
    }
    private Movement currentMovement = Movement.sleep;
    private Animator anim;

    public void changeMovement(Movement move)
    {
        if(move!=Movement.idle)
            anim.SetBool("isIdle", false);
        switch (move)
        {
            case Movement.sleep:
                anim.SetFloat("State", 0f);
                break;
            case Movement.standUp:
                anim.SetFloat("State", 0.5f);
                
                break;
            case Movement.Walk:
                anim.SetFloat("State", 1f);
                break;
            case Movement.idle:
                anim.SetBool("isIdle", true);
                break;
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("State", 0f);
    }
    

}
