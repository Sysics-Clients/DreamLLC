using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SniperStates : MonoBehaviour
{
    public SniperBehavior sniperBehavior;
    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public enum State
    {
        Idle,
        Roaming,
        Attack,
        Chase,
        Death
    }
    private void OnEnable()
    {
        GeneralEvents.stopEnemies += StopShooting;
        sniperBehavior.changeGun += changeGun;
        sniperBehavior.getState += getCurrentState;
        sniperBehavior.changeState += changeState;
        sniperBehavior.toIdle += toIdle;
    }
    private void OnDisable()
    {
        GeneralEvents.stopEnemies -= StopShooting;
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
    private int currentGun=0;//2: attack gun ,0:idle gun,1:roaming gun  , -1: no Gun
    public List<GameObject> guns;
    GameObject player;
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
        player = GameObject.FindGameObjectWithTag("Player");
        anim.SetBool("isShooting", false);
        guns[0].SetActive(true);
    }
    void StopShooting()
    {
        anim.SetBool("isShooting", false);
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
    void FixedUpdate()
    {
        if (!sniperBehavior.isVisible)
            return;
        print(currentState);
        switch (currentState)
        {
            case State.Idle:
                if ((Vector3.Distance(transform.position, Positions[currentPos].position) < 40f) && Mathf.Abs(transform.position.y - player.transform.position.y) < 0.5f)
                {
                    transform.LookAt(player.transform.position);
                    sniperBehavior.enemyMovement(SniperMovement.Movement.ThrowGrenade);
                    agent.speed = 0;
                    changeState(State.Attack);
                }
                break;
            case State.Roaming:
                if (Vector3.Distance(transform.position, Positions[currentPos].position) < 1f) //Reach Destination
                {
                    toIdle();
                }
                if((Vector3.Distance(transform.position, player.transform.position) < 30f)&& Mathf.Abs( transform.position.y- player.transform.position.y)<0.5f)
                {
                    transform.LookAt(player.transform.position);
                    sniperBehavior.enemyMovement(SniperMovement.Movement.ThrowGrenade);
                    agent.speed = 0;
                    changeState(State.Attack);
                }
                    break;
            case State.Attack:
                transform.LookAt(player.transform.position);
                break;
            case State.Chase:
                if (!agent.hasPath && LookAtPlayer)
                {
                    transform.LookAt(player.transform.position);
                    sniperBehavior.enemyMovement(SniperMovement.Movement.ThrowGrenade);
                    agent.speed = 0;
                    changeState(State.Attack);
                }
                break;
            case State.Death:
                audioManager.PlaySound(AudioManager.Sounds.enemyDie);
                StopAllCoroutines();
                sniperBehavior.EnemyCanvas.enabled = false;
                anim.SetBool("isShooting", false);
                sniperBehavior.enemyMovement(SniperMovement.Movement.Die);
                agent.speed = 0;
                guns[currentGun].SetActive(false);
                enabled = false;
                MissionObjects mo = GetComponent<MissionObjects>();
                if (mo != null)
                    GeneralEvents.onTaskFinish(MissionName.destroyEnemy, mo.id);
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
