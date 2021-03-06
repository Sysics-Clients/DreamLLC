using System.Collections;
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

    public Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    public float speedRotation;
    [Range(0,1)]
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
        StartCoroutine(FOVRoutine());
        slow = 1;

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
        GeneralEvents.setSpeed += setSpeed;
      Weapon[] w= gameObject.GetComponent<Attack>().weapons;
      foreach(var item in w)
      {
          if(item.weap.activeInHierarchy)
          {
              speed=item.weaponItem.speed;
          }
      }
    }
    private void OnDisable()
    {
        playerBehavior.state -= changeState;
        GeneralEvents.sendMvt -= GetMvt;
        GeneralEvents.sendShooting -= GetDir;
        playerBehavior.getState -= getState;
        playerBehavior.die -= die;
        GeneralEvents.sendRoll -= GetRoll;
        GeneralEvents.setSpeed -= setSpeed;

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
        /*
        if (Input.GetKeyUp(KeyCode.W))
        {
            changeMvt();
        }
        */
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
            animator.SetFloat("speed", slow);
            if (slow <1)
            {
                slow += 0.1f;
            }
            
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
            if (target != null)
            {
                if(target.position.y <transform.position.y+1&& target.position.y > transform.position.y - 1)
                    transform.LookAt(target);
            }
            else if (ShootingDir != Vector3.zero&&ShootingDir.magnitude>0.5f)
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
        WaitForSeconds wait = new WaitForSeconds(0.5f);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        List<Collider> colliders = new List<Collider>();
        foreach (var item in rangeChecks)
        {
            if (item.gameObject.tag!="Drone")
            {
                colliders.Add(item);
            }
        }
        if (colliders.Count != 0)
        {
            target = colliders[0].transform;
            
        }
        else
        {
            target = null;
        }
    }

    private void die()
    {
        audio.clip = null;
        animator.SetBool("die", true);
        this.enabled = false;
    }
    public void setSpeed(float v)
    {
        speed = v;
    }
}
