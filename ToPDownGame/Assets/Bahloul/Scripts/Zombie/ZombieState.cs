using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieState : MonoBehaviour
{
    public ZombieBehavior zombieBehavior;
    private AudioManager audioManager;
    float distance;
    public Vector3 startPos;
    private void OnEnable()
    {
        gameObject.tag="zombie";
        gameObject.layer=8;
        zombieBehavior.enemyState += changeState;

    }
    private void OnDisable()
    {
        zombieBehavior.enemyState -= changeState;
    }

    public enum State
    {
        Idle,
        Chasing,
        Attack,
        Death,
    }
    public ZombieState.State getCurrentState() { return currentState; }
    public void changeState(State s) { currentState = s; }
    public float distanceToAttack=2;
    Animator anim;
    private bool startRound;

    private State currentState;

    NavMeshAgent agent;
    

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        startRound = false;
        startPos = transform.position;
        StartCoroutine(startWave());
    }
    
    IEnumerator startWave()
    {
        yield return new WaitForSeconds(2);
        startRound = true;
    }

    void toChase()
    {
        currentState = State.Chasing;
        agent.speed = 2;
        zombieBehavior.enemyMovement(ZombieMvt.Movement.Walk);
        agent.isStopped = false;
       
    }
    void toIdle()
    {
        currentState = State.Idle;
        zombieBehavior.enemyMovement(ZombieMvt.Movement.Idle);
        agent.isStopped = true;
    }
    void toAttack()
    {
        zombieBehavior.enemyMovement(ZombieMvt.Movement.Attack);
        currentState = State.Attack;
        agent.isStopped = true;
    }

    void toDie()
    {

        audioManager.PlaySound(AudioManager.Sounds.enemyDie);
        zombieBehavior.enemyMovement(ZombieMvt.Movement.Die);
        agent.speed = 0;
        gameObject.tag = "Untagged";
        gameObject.layer=0;
        zombieBehavior.disableCanvas();
        
    }
    public void FinishDying()
    {
        zombieBehavior.enemyMovement(ZombieMvt.Movement.Idle);
        transform.position = startPos;
        gameObject.tag = "zombie";
        zombieBehavior.enableCanvas();
        gameObject.SetActive(false);
        if (zombiesManager.instance.testActiveZombies())
        {
            print(zombiesManager.instance.currentWave);
            GeneralEvents.onTaskFinish(MissionName.KillAllZombies,zombiesManager.instance.currentWave+1);
            print(zombiesManager.instance.currentWave);

        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currentState)
        {
            
            case State.Idle:
                   transform.LookAt(new Vector3(zombieBehavior.player.transform.position.x,transform.position.y, zombieBehavior.player.transform.position.z));
                if (startRound)
                {
                    toChase();
                }
                break;
            case State.Chasing:
                
                distance = Vector3.Distance(transform.position, zombieBehavior.player.transform.position);
               // transform.LookAt(new Vector3(zombieBehavior.player.transform.position.x, transform.position.y, zombieBehavior.player.transform.position.z));
                if (distance < distanceToAttack)
                {
                    toAttack();
                    return;
                }
                else
                {
                    currentState=State.Chasing;
                }
                agent.SetDestination(zombieBehavior.player.transform.position);
                break;
            case State.Attack:
                transform.LookAt(new Vector3(zombieBehavior.player.transform.position.x, transform.position.y, zombieBehavior.player.transform.position.z));
                distance = Vector3.Distance(transform.position, zombieBehavior.player.transform.position);
                if (distance >= distanceToAttack)
                {
                    toChase();
                }
                break;
            case State.Death:
                toDie();
                break;
        }
    }
}
