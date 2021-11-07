using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieState : MonoBehaviour
{
    public ZombieBehavior zombieBehavior;
    private AudioManager audioManager;
    float distance;
    Vector3 startPos;
    private void OnEnable()
    {


    }
    private void OnDisable()
    {

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
        agent.enabled = true;
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
        MissionObjects mo = GetComponent<MissionObjects>();
        if (mo != null)
            GeneralEvents.onTaskFinish(MissionName.destroyEnemy, mo.id);
        gameObject.tag = "Untagged";
        gameObject.layer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        print(currentState);
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
                transform.LookAt(new Vector3(zombieBehavior.player.transform.position.x, transform.position.y, zombieBehavior.player.transform.position.z));
                if (distance < distanceToAttack)
                {
                    toAttack();
                    return;
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
