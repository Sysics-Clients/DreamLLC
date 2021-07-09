using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStates : MonoBehaviour

{
    public EnemyBehavior enemyBehavior;


    private void OnEnable()
    {
        enemyBehavior.enemyState += changeState;
    }
    private void OnDisable()
    {
        enemyBehavior.enemyState -= changeState;
    }

    public enum State
    {
        Idle,
        Roaming,
        Chasing,
        Attack,
        Death,
    }
    public void changeState(State s) { currentState = s; }
    Animator anim;
    Transform playerTransform;
    public List<Sprite> EnemyStatesSprites;
    public Image StateImage;
    Coroutine WaitIdle;
    private State currentState;
    private int currentPos=0;
    [SerializeField]
    private float timeToWait;
    public List<Vector3> Positions ;
    public List<GameObject> listGuns;
    NavMeshAgent agent;
    private int activeGun;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Positions.Add(transform.position);
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Idle;
        WaitIdle = StartCoroutine(WaitOnIdle());
        StateImage.sprite = EnemyStatesSprites[0];
        activeGun = 0;
        listGuns[0].SetActive(true);
    }
    void toChase() {
        StateImage.sprite = EnemyStatesSprites[1];
        currentState = State.Chasing;
        agent.speed = enemyBehavior.Item.runSpeed;
        enemyBehavior.enemyMovement(EnemyController.Movement.Run);
        changeGun(1);
    }
    private void changeGun(int i) {
        listGuns[activeGun].SetActive(false);
        activeGun = i;
        listGuns[activeGun].SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {   
        switch (currentState) {
            case State.Idle:
                if (enemyBehavior.fieldOfView())
                {
                    StopCoroutine(WaitIdle);
                    toChase();
                }

                break;
            case State.Roaming:
                if (enemyBehavior.fieldOfView())
                {
                    toChase();

                }
                agent.SetDestination(Positions[currentPos]);
                if (Vector3.Distance(transform.position, Positions[currentPos]) < 0.1f) //Reach Destination"
                {
                    currentState = State.Idle;
                    enemyBehavior.enemyMovement(EnemyController.Movement.Idle);
                    changeGun(0);
                    WaitIdle = StartCoroutine(WaitOnIdle());
                    
                }
                break;
            case State.Chasing:
                if (Vector3.Distance(transform.position, playerTransform.position) > 10)
                {
                    currentState = State.Roaming;
                    enemyBehavior.enemyMovement(EnemyController.Movement.Walk);
                    agent.speed = enemyBehavior.Item.walkSpeed;

                }
                else if (Vector3.Distance(transform.position, playerTransform.position) > 5)
                {
                    agent.SetDestination(playerTransform.position);
                }
                else {
                    StateImage.sprite = EnemyStatesSprites[2];
                    currentState = State.Attack;
                    anim.SetBool("isShooting", true);
                    agent.speed = 0;
                    enemyBehavior.enemyMovement(EnemyController.Movement.Idle);

                    changeGun(2);
                }
                break;
            case State.Attack:
                if (Vector3.Distance(transform.position, playerTransform.position) > 5) {
                    toChase();
                    anim.SetBool("isShooting", false);
                }
                else
                {
                    transform.LookAt(new Vector3( playerTransform.position.x,transform.position.y, playerTransform.position.z));
                }
                break;
            case State.Death:
                anim.SetBool("isShooting", false);
                anim.SetBool("Die", true);
                agent.speed = 0;
                listGuns[activeGun].SetActive(false);
                this.enabled = false;
                break;
        }
    }
    private void setPosition()
    {
        if (currentPos < (Positions.Count - 1))        
            currentPos++;            
        else
        currentPos = 0;

    }
    private IEnumerator WaitOnIdle()
    {
            yield return new WaitForSeconds(timeToWait);
            setPosition();
            currentState = State.Roaming;
        enemyBehavior.enemyMovement(EnemyController.Movement.Walk);

        changeGun(1);
    }
}
