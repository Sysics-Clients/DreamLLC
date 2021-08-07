using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SniperStates : MonoBehaviour
{
    public SniperBehavior sniperBehavior;
    
    public enum State
    {
        Idle,
        Roaming,
        Attack,
        Chase,
    }
    private void OnEnable()
    {
        sniperBehavior.changeGun += changeGun;
        sniperBehavior.getState += getCurrentState;
        sniperBehavior.changeState += changeState;
        sniperBehavior.toIdle += toIdle;
    }
    private void OnDisable()
    {
        sniperBehavior.changeGun -= changeGun;
        sniperBehavior.getState -= getCurrentState;
        sniperBehavior.changeState -= changeState;
        sniperBehavior.toIdle -= toIdle;
    }
    public SniperStates.State getCurrentState() { return currentState; }
    public void changeGun(int i) {
        if (currentGun != -1)
        {
            guns[currentGun].SetActive(false);
        }
        if(i!=-1)
            guns[i].SetActive(true);
        currentGun = i;
    }
    public void changeState(State s) { currentState = s; }
    private State currentState;
    private int currentGun=1;//2: attack gun ,0:idle gun,1:roaming gun  , -1: no Gun
    public List<GameObject> guns;
    Transform playerTransform;
    Animator anim;
    Coroutine WaitIdle;
    [SerializeField] float timeToWaitIdle;
    public List<Transform> Positions;
    NavMeshAgent agent;
    private int currentPos = 0;
    bool LookAtPlayer = false;
    // Start is called before the first frame update
   
    void Start()
    {
        currentState = State.Idle;
        WaitIdle = StartCoroutine(WaitOnIdle());
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        anim.SetBool("isShooting", false);
        guns[0].SetActive(true);
    }
    public void toHelp(Vector3 pos)
    {
        StopCoroutine(WaitIdle);
        sniperBehavior.enemyMovement(SniperMovement.Movement.Walk);
        agent.SetDestination(pos);
        LookAtPlayer = true;
        changeState(State.Chase);
    }
    void toIdle()
    {
        currentState = State.Idle;
        sniperBehavior.enemyMovement(SniperMovement.Movement.Idle);
        agent.speed = 0;
        WaitIdle = StartCoroutine(WaitOnIdle());
        anim.SetBool("isShooting", false);
    }
    void toRoaming()
    {
        currentState = State.Roaming;
        sniperBehavior.enemyMovement(SniperMovement.Movement.Walk);
        agent.SetDestination(Positions[currentPos].position);
        agent.speed = sniperBehavior.Item.walkSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        if (!sniperBehavior.isVisible)
            return;

        switch (currentState)
        {
            case State.Idle:
               
                break;
            case State.Roaming:
                if (Vector3.Distance(transform.position, Positions[currentPos].position) < 0.5f) //Reach Destination
                {
                    toIdle();
                }
                break;
            case State.Attack:
                break;
            case State.Chase:
                if (!agent.hasPath && LookAtPlayer)
                {
                    transform.LookAt(playerTransform.position);
                    sniperBehavior.enemyMovement(SniperMovement.Movement.ThrowGrenade);
                    agent.speed = 0;
                    changeState(State.Attack);
                }
                break;

        }
        }

    private IEnumerator WaitOnIdle()
    {
        yield return new WaitForSeconds(timeToWaitIdle);
        setPosition();
        toRoaming();
    }
    private void setPosition()
    {
        if (currentPos < (Positions.Count - 1))
            currentPos++;
        else
            currentPos = 0;
    }
}
