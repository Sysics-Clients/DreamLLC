using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentControler : MonoBehaviour
{
    public CharacterController characterController;

    public float speed=12;
    public float gravity=-9.8f;
    public Transform groundCheck;
    public float groundDistance=0.4f;
    public LayerMask mask;
    Vector3 velocity;
    bool isGrounded;
    Animator animator;
    public Joystick joystick;

    private State _courentState;
    public PlayerBehavior playerBehavior;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _courentState = State.walk;
    }

    private void OnEnable()
    {
        playerBehavior.state += changeState;
    }
    private void OnDisable()
    {
        playerBehavior.state -= changeState;
    }
    // Update is called once per frame
    
    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, mask);
        if (isGrounded && velocity.y<0)
        {
            velocity.y = -2;
        }

        float x = -joystick.Vertical;
        float y = joystick.Horizontal;

        switch (_courentState)
        {
            case State.walk:
                {
                    animator.SetBool("attack", false);
                    animator.SetFloat("speed", y / 2);
                    Vector3 move = transform.right * x + transform.forward * y;
                    characterController.Move(move * speed * Time.deltaTime);
                    break;
                }
            case State.run: 
                {
                    animator.SetBool("attack", false);
                    animator.SetFloat("speed", y);
                    Vector3 move = transform.right * x + transform.forward * y;
                    characterController.Move(move * speed * Time.deltaTime);
                    break;
                }
            case State.attack:
                {
                    animator.SetBool("attack", true);
                    _courentState = State.walk;
                    break;
                }
            case State.die:
                {
                    animator.SetBool("die", true);
                    break;
                }
        }
        

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void changeState(State state)
    {
        _courentState = state;
    }

    public void run()
    {
        if (_courentState == State.walk)
        {
            _courentState = State.run;
            speed = 18;
        }
        else 
        {
            _courentState = State.walk;
            speed = 12;
        }
    }

    public enum State
    {
        walk,
        run,
        attack,
        die
    }

}
