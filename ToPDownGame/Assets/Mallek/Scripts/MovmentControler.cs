using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentControler : MonoBehaviour
{
    public CharacterController characterController;

    public float speed=12;

    Animator animator;
    public Joystick joystick;

    private State _courentState;
    public PlayerBehavior playerBehavior;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    public float speedRotation;

    float slow;
    Transform target;
    public float radius;
    public LayerMask targetMask;


    // Start is called before the first frame update
    void Start()
    {
        joystick = GameObject.FindObjectOfType<FixedJoystick>();
        animator = GetComponent<Animator>();
        _courentState = State.walk;
        StartCoroutine(FOVRoutine());
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

    /* void FixedUpdate()
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
     }*/

    private void Update()
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
        Vector3 move = new Vector3(joystick.Horizontal, 0, joystick.Vertical);


        //animator.SetFloat("speed", Mathf.Abs(move.magnitude * Time.deltaTime * speed));
        //animator.SetFloat("speed", 1);
        if (_courentState == State.roll)
        {
            characterController.Move(transform.forward * Time.smoothDeltaTime *10);
        }
        else if (move != Vector3.zero)
        {
            move.y = 0;
            characterController.Move(move.normalized * Time.smoothDeltaTime * speed);
            animator.SetFloat("speed", 1);
            slow = 1;
        }
        else
        {
            if(slow>0.01f)
                slow = slow*3 / 4;
            
            animator.SetFloat("speed", slow);
        }
        if (target != null&&_courentState!=State.roll)
        {
            LockOnTarget((target.position- transform.position).normalized);
        }
        else if (move != Vector3.zero && transform.forward.normalized != move.normalized )
        {
            LockOnTarget(move.normalized);

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

    public void stopRoll()
    {
        _courentState = State.walk;
      
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
}
