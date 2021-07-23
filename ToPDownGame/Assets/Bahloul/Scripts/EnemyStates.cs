﻿using System.Collections;
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
        enemyBehavior.toHide += toHide;
        enemyBehavior.getCurrentState += getCurrentState;

    }
    private void OnDisable()
    {
        enemyBehavior.enemyState -= changeState;
        enemyBehavior.toHide -= toHide;
        enemyBehavior.getCurrentState -= getCurrentState;
    }

    public enum State
    {
        Idle,
        Roaming,
        Chasing,
        Attack,
        Death,
    }
    public EnemyStates.State getCurrentState() { return currentState; }
    public void changeState(State s) { currentState = s; }
    public LayerMask ObstacleLayer;
    public LayerMask EnemyLayer;
    Animator anim;
    Transform playerTransform;
    public List<Sprite> EnemyStatesSprites;
    public Image StateImage;
    Coroutine WaitIdle;
    Coroutine WaitHide;
    private State currentState;
    private int currentPos=0;
    [SerializeField]
    private float timeToWaitIdle;
    [SerializeField]
    private float timeToWaitHide;
    public List<Transform> Positions ;
    public List<GameObject> listGuns;
    NavMeshAgent agent;
    private int activeGun;
    private Vector3 LastPlayerPosition;
    private Vector3 PosToHide;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Positions.Add(transform);
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Idle;
        WaitIdle = StartCoroutine(WaitOnIdle());
        StateImage.sprite = EnemyStatesSprites[0];
        activeGun = 0;
        listGuns[0].SetActive(true);
        LastPlayerPosition = playerTransform.position;
    }
    public void toHelp(Vector3 position) {

        anim.SetBool("isShooting", false);
        agent.SetDestination(position);
        enemyBehavior.enemyMovement(EnemyController.Movement.Run);
        enemyBehavior.setEnemyFovColor(Color.yellow);
        StateImage.sprite = EnemyStatesSprites[1];
        currentState = State.Idle;
        agent.speed = enemyBehavior.Item.runSpeed;
        changeGun(1);

    }
    private void callForHelp() {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, 15, EnemyLayer);
        if (rangeChecks.Length != 0)
        {

            for (int i = 0; i < rangeChecks.Length; i++)
            {
                rangeChecks[i].GetComponent<EnemyStates>().toHelp(transform.position);
            }
        }
    }
    void toHide() {
        LastPlayerPosition = playerTransform.position;
        callForHelp();
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, 10, ObstacleLayer);
        if (rangeChecks.Length != 0) {
            PosToHide = rangeChecks[0].transform.position;
            float maxDistance = Vector3.Distance(rangeChecks[0].transform.position, playerTransform.position);
            float currentDistance;
            for (int i=1;i< rangeChecks.Length; i++)
            {
                currentDistance = Vector3.Distance(rangeChecks[i].transform.position, playerTransform.position);
                if (maxDistance < currentDistance)
                {
                    maxDistance = currentDistance;
                    PosToHide = rangeChecks[i].transform.position;
                }
            }
            anim.SetBool("isShooting", false);
            agent.SetDestination(PosToHide);
            enemyBehavior.enemyMovement(EnemyController.Movement.Run);
            enemyBehavior.setEnemyFovColor(Color.yellow);
            StateImage.sprite = EnemyStatesSprites[1];
            currentState = State.Idle;
            agent.speed = enemyBehavior.Item.runSpeed;
            changeGun(1);
        }
    }
    void toChase() {
        enemyBehavior.setEnemyFovColor(Color.yellow);
        StateImage.sprite = EnemyStatesSprites[1];
        currentState = State.Chasing;
        agent.speed = enemyBehavior.Item.runSpeed;
        enemyBehavior.enemyMovement(EnemyController.Movement.Run);
        changeGun(1);
    }
    void toIdle() {
        currentState = State.Idle;
        enemyBehavior.enemyMovement(EnemyController.Movement.Idle);
        changeGun(0);
        agent.speed = 0;
        WaitIdle = StartCoroutine(WaitOnIdle());
    }
    void toAttack() {
        enemyBehavior.setEnemyFovColor(Color.red);
        StateImage.sprite = EnemyStatesSprites[2];
        currentState = State.Attack;
        anim.SetBool("isShooting", true);
        agent.speed = 0;
        enemyBehavior.enemyMovement(EnemyController.Movement.Idle);
        changeGun(2);
    }
    void toRoaming() {
        currentState = State.Roaming;
        enemyBehavior.enemyMovement(EnemyController.Movement.Walk);
        agent.SetDestination(Positions[currentPos].position);
        changeGun(1);
        agent.speed = enemyBehavior.Item.walkSpeed;
        StateImage.sprite = EnemyStatesSprites[1];

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
                if (enemyBehavior.canSeeThePlayer())
                {
                    StopCoroutine(WaitIdle);
                    StopCoroutine(WaitHide);
                    toAttack();
                }
                break;
            case State.Roaming:
                
                if (enemyBehavior.canSeeThePlayer())
                {
                    toAttack();
                }
                if (Vector3.Distance(transform.position, Positions[currentPos].position) < 0.1f) //Reach Destination"
                {
                    toIdle();
                }
                break;
            case State.Chasing:
                print("chasing");
                float distance = Vector3.Distance(transform.position, playerTransform.position);
                bool check = enemyBehavior.checkLongRange(13,180);
                if (enemyBehavior.canSeeThePlayer())
                {
                    toAttack();
                    break;
                }
                if ((distance < 13)&&(check==true))
                {
                    agent.SetDestination(playerTransform.position);
                    LastPlayerPosition = playerTransform.position;
                }
                else {
                    agent.SetDestination(LastPlayerPosition);
                    enemyBehavior.setEnemyFovColor(Color.yellow);
                    if (Vector3.Distance(transform.position, LastPlayerPosition) < 0.1f) //Reach destination
                    {
                        toIdle();
                    }
                }
                break;
            case State.Attack:
                print("attack");
                if (enemyBehavior.canSeeThePlayer())
                {
                    transform.LookAt(new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z));
                    LastPlayerPosition = playerTransform.position;
                    break;
                }
                else {
                    toChase();
                    anim.SetBool("isShooting", false);
                }
                break;
            case State.Death:
                anim.SetBool("isShooting", false);
                enemyBehavior.enemyMovement(EnemyController.Movement.Die);
                agent.speed = 0;
                listGuns[activeGun].SetActive(false);
                enemyBehavior.disableOrEnableFieldOfView(false);
                enabled = false;
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
            yield return new WaitForSeconds(timeToWaitIdle);
            setPosition();
            toRoaming();
    }
    private IEnumerator WaitOnHide()
    {
        transform.LookAt(playerTransform);
        yield return new WaitForSeconds(timeToWaitHide);
        toAttack();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hider")
        {
            changeState(State.Idle);
            changeGun(0);
            agent.speed = 0;
            enemyBehavior.enemyMovement(EnemyController.Movement.Crouch);
            transform.LookAt(playerTransform.position);
            WaitHide= StartCoroutine(WaitOnHide());
        }
    }
}
