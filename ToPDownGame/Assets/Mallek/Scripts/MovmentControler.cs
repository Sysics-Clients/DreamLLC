﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentControler : MonoBehaviour
{
    public CharacterController characterController;

    public float speed ;

    Animator animator;
    public Joystick joystick;

    public State _courentState;
    public PlayerBehavior playerBehavior;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    public float speedRotation;

    float slow;
    Transform target;
    public float radius;
    public LayerMask targetMask;
    Vector3 move;
    Vector3 ShootingDir;
    bool crouch;

    public AudioSource audio;
    public AudioClip walk;

    // Start is called before the first frame update
    void Start()
    {
        
        joystick = GameObject.FindObjectOfType<FixedJoystick>();
        animator = GetComponent<Animator>();
        _courentState = State.walk;
        //StartCoroutine(FOVRoutine());

    }

    private void OnEnable()
    {
        Application.targetFrameRate = 60;
        playerBehavior.state += changeState;
        GeneralEvents.sendMvt += GetMvt;
        GeneralEvents.sendShooting += GetDir;
        playerBehavior.getState += getState;
        playerBehavior.die += die;
        GeneralEvents.sendRoll += GetRoll;
    }
    private void OnDisable()
    {
        playerBehavior.state -= changeState;
        GeneralEvents.sendMvt -= GetMvt;
        GeneralEvents.sendShooting -= GetDir;
        playerBehavior.getState -= getState;
        playerBehavior.die -= die;
        GeneralEvents.sendRoll -= GetRoll;

    }


    private void FixedUpdate()
    {
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        //Test roll On PC
        if (Input.GetKeyUp(KeyCode.A))
        {
            roll();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            changeMvt();
        }

        //animator.SetFloat("speed", Mathf.Abs(move.magnitude * Time.deltaTime * speed));
        //animator.SetFloat("speed", 1);
        if (_courentState == State.roll)
        {
            characterController.Move(transform.forward * Time.smoothDeltaTime *speed*1.5f);
            
        }
        else if (move != Vector3.zero)
        {
            audio.clip = walk;
            if (!audio.isPlaying)
            {
                audio.Play();
            }
            move.y = 0;
            characterController.Move(move.normalized * Time.smoothDeltaTime * speed);
            animator.SetFloat("speed", 1);
            slow = 1;
        }
        else
        {
            if (audio.clip==walk)
            {
                audio.Stop();
            }
            
            if (slow > 0.01f)
                slow = slow * 3 / 4;

            animator.SetFloat("speed", slow);
        }
        if (_courentState != State.roll)
        {
            if (ShootingDir != Vector3.zero&&ShootingDir.magnitude>0.5f)
            {
                LockOnTarget(ShootingDir.normalized);
            }
            else if (move != Vector3.zero && transform.forward.normalized != move.normalized)
            {
                LockOnTarget(move.normalized);

            }
        }

        if (groundedPlayer == false)
        {
            playerVelocity.y = gravityValue * Time.deltaTime;
            characterController.Move(playerVelocity);
        }
    }

    void LockOnTarget(Vector3 _target)
    {
        Quaternion startrotation = new Quaternion(0, 0, 0, 0);
        Vector3 root = Vector3.Lerp(transform.forward, _target, speedRotation);
        transform.forward = root;
    }

    public void roll()
    {
        animator.SetTrigger("roll");
        _courentState = State.roll;
    }
    public void changeMvt()
    {
        if (!crouch)
        {
            animator.SetBool("crouch", true);
            speed = 1;
            crouch = !crouch;
        }
        else
        {
            animator.SetBool("crouch", false);
            speed = 3;
            crouch = !crouch;
        }
    }

    public State getState()
    {
        return _courentState;
    }
    public void stopRoll()
    {
        _courentState = State.walk;
    }

    private void changeState(State state)
    {
        _courentState = state;
    }

    //Get Movement from InputSystem
     void GetMvt(Vector3 m)
    {
        move = m;
    }
    //Get Direction Shooting from InputSystem
     void GetDir(Vector3 dir)
    {
        ShootingDir = dir;
    }

    // Get Roll State
    void GetRoll()
    {
        roll();
    }

    public enum State
    {
        walk,
        run,
        roll,
        die
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            target = rangeChecks[0].transform;
        }
        else
        {
            target = null;
        }
    }

    private void die()
    {
        animator.SetBool("die", true);
        this.enabled = false;
    }
}
