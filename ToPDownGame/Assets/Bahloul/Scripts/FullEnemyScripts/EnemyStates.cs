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
        Hide,
    }
    public EnemyStates.State getCurrentState() { return currentState; }
    public void changeState(State s) { currentState = s; }
    public LayerMask HideObstacleLayer;
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
    private float timeToWaitCrouch;
    [SerializeField]
    private float timeToWaitHide;
    public List<Transform> Positions ;
    public List<GameObject> listGuns;
    NavMeshAgent agent;
    private int activeGun;
    private Vector3 LastPlayerPosition;
    private Vector3 PosToHide;
    private bool isHiding=false;
    private bool runAway = false;
    private bool LookAtPlayer = false;
    public bool RunScript;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        LastPlayerPosition = playerTransform.position;
        if (enemyBehavior.Item.enemyName != "Shooters")
        {
            currentState = State.Idle;
            WaitIdle = StartCoroutine(WaitOnIdle());      
            Positions.Add(transform);
            StateImage.sprite = EnemyStatesSprites[0];
            activeGun = 0;
            listGuns[0].SetActive(true);
        }
        else
        {
            toAttack();
        }
    }
    public void toHelp(Vector3 position) {

        anim.SetBool("isShooting", false);
        agent.SetDestination(position);
        enemyBehavior.enemyMovement(EnemyController.Movement.Run);
        enemyBehavior.setEnemyFovColor(Color.yellow);
        StateImage.sprite = EnemyStatesSprites[1];
        currentState = State.Hide;
        agent.speed = enemyBehavior.Item.runSpeed;
        changeGun(1);
        StartCoroutine(CheckDistance(position,3));

    }
    private void callForHelp() {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, 20, EnemyLayer);
        if (rangeChecks.Length > 1)
        {
            for (int i = 0; i < rangeChecks.Length; i++)
            {
                if((rangeChecks[i].name!=gameObject.name)||(Vector3.Distance(transform.position,rangeChecks[i].transform.position)>5))
                    rangeChecks[i].GetComponent<EnemyStates>().toHelp(transform.position);
            }
        }
    }
    void toHide() {
        if (!isHiding)
        {
            if (WaitIdle != null)
                StopCoroutine(WaitIdle);
            if (WaitHide != null)
                StopCoroutine(WaitHide);
            LastPlayerPosition = playerTransform.position;
            callForHelp();
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, 15, HideObstacleLayer);
            
            if (rangeChecks.Length != 0)
            {
                PosToHide = rangeChecks[0].transform.position;
                float maxDistance = Vector3.Distance(rangeChecks[0].transform.position , playerTransform.position);
                float currentDistance;
                for (int i = 0; i < rangeChecks.Length; i++)
                {

                    currentDistance = Vector3.Distance(rangeChecks[i].transform.position , playerTransform.position);
                    //print(i + " " + maxDistance + "    " + currentDistance);
                    //print(rangeChecks[i].gameObject.name);
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
                currentState = State.Hide;
                agent.speed = enemyBehavior.Item.runSpeed;
                changeGun(1);

            }
            else
            {
                bool goAskHelp = false;
                rangeChecks = Physics.OverlapSphere(transform.position, 30, EnemyLayer);
                if (rangeChecks.Length >1)
                {
                    for (int i = 0; i < rangeChecks.Length; i++) {
                        print(rangeChecks[i].name);
                        if (rangeChecks[i].gameObject != this.gameObject)
                        {
                            anim.SetBool("isShooting", false);
                            agent.SetDestination(rangeChecks[0].transform.position);
                            enemyBehavior.enemyMovement(EnemyController.Movement.Run);
                            enemyBehavior.setEnemyFovColor(Color.yellow);
                            StateImage.sprite = EnemyStatesSprites[1];
                            currentState = State.Hide;
                            agent.speed = enemyBehavior.Item.runSpeed;
                            changeGun(1);
                            print("going to a friend");
                            goAskHelp = true;
                            StartCoroutine(CheckDistance(rangeChecks[i].gameObject.transform.position,3));
                            LookAtPlayer = false;
                            break;
                        }
                    }
                }
                if(!goAskHelp)
                {
                    anim.SetBool("isShooting", false);
                    agent.SetDestination(2* transform.position- playerTransform.position);
                    enemyBehavior.enemyMovement(EnemyController.Movement.Run);
                    enemyBehavior.setEnemyFovColor(Color.yellow);
                    StateImage.sprite = EnemyStatesSprites[1];
                    currentState = State.Hide;
                    agent.speed = enemyBehavior.Item.runSpeed;
                    changeGun(1);
                    runAway = true;
                    StartCoroutine(CheckDistance(2 * transform.position - playerTransform.position,1));
                }
            }
        }
        else {
            print("Already Hiding");
            enemyBehavior.enemyMovement(EnemyController.Movement.Crouch);
            enemyBehavior.disableOrEnableRenderingFov(false);
            changeGun(3);
            anim.SetBool("isShooting", false);
            StartCoroutine(WaitOnCrouch());
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
        if (!enemyBehavior.isVisible)
            return;
        switch (currentState) {
            case State.Idle:
                //print("Idle " + gameObject.name);
                if (enemyBehavior.canSeeThePlayer())
                {
                    if(WaitIdle!=null)
                    StopCoroutine(WaitIdle);
                    toAttack();
                }
                if(LookAtPlayer)
                {
                    transform.LookAt(playerTransform.position);
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
                if (enemyBehavior.Item.enemyName != "Shooters")
                {
                    if (enemyBehavior.canSeeThePlayer())
                    {
                        transform.LookAt(new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z));
                        LastPlayerPosition = playerTransform.position;
                        break;
                    }
                    else
                    {
                        toChase();
                        anim.SetBool("isShooting", false);
                    }
                }
                else
                {
                    transform.LookAt(new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z));
                    LastPlayerPosition = playerTransform.position;
                }
                break;
            case State.Death:
                StopAllCoroutines();
                enemyBehavior.EnemyCanvas.enabled = false;
                anim.SetBool("isShooting", false);
                enemyBehavior.enemyMovement(EnemyController.Movement.Die);
                agent.speed = 0;
                listGuns[activeGun].SetActive(false);
                enemyBehavior.disableOrEnableRenderingFov(false);
                enabled = false;
                break;

            case State.Hide:
                LastPlayerPosition = playerTransform.position;
                if ((isHiding)||(LookAtPlayer))
                {
                    transform.LookAt(playerTransform.position);
                }

                if (enemyBehavior.canSeeThePlayer())
                {
                    enemyBehavior.setEnemyFovColor(Color.red);
                    StateImage.sprite = EnemyStatesSprites[2];
                }
                else {
                    enemyBehavior.setEnemyFovColor(Color.yellow);
                    StateImage.sprite = EnemyStatesSprites[1];
                    distance = Vector3.Distance(transform.position, playerTransform.position);
                    check = enemyBehavior.checkLongRange(20, 180);

                    if ((distance > 20) || (check == false))
                    {
                        StartCoroutine(WaitOnHide());
                        currentState = State.Idle;
                    }
                }
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
    private IEnumerator WaitOnCrouch()
    {
        yield return new WaitForSeconds(timeToWaitCrouch);
        enemyBehavior.enemyMovement(EnemyController.Movement.Idle);
        anim.SetBool("isShooting", true);
        changeGun(2);
        enemyBehavior.disableOrEnableRenderingFov(true);
    }
    private IEnumerator WaitOnHide()
    {
        yield return new WaitForSeconds(timeToWaitHide);
        enemyBehavior.enemyMovement(EnemyController.Movement.Idle);
        toChase();
        LookAtPlayer = false;
    }
    private IEnumerator WaitOnIdle()
    {
            yield return new WaitForSeconds(timeToWaitIdle);
            setPosition();
            toRoaming();
    }
    private IEnumerator CheckDistance(Vector3 dis,float DistanceBetween)
    {
        
            yield return new WaitForSeconds(0.1f);
        if (Vector3.Distance(transform.position, dis) < DistanceBetween) //Reach destination
            {
            
                enemyBehavior.enemyMovement(EnemyController.Movement.Idle);
                changeState(State.Hide);
                changeGun(0);
                agent.speed = 0;
                transform.LookAt(playerTransform.position);
                anim.SetBool("isShooting", true);
                changeGun(2);
                runAway = false;
            }
        if (runAway == true)
        {
            StartCoroutine(CheckDistance(dis,1));
        }
        else
        {
            LookAtPlayer = true;
        }
                
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hider")
        {
            enemyBehavior.enemyMovement(EnemyController.Movement.Idle);
            changeState(State.Hide);
            changeGun(0);
            agent.speed = 0;
            transform.LookAt(playerTransform.position);
            isHiding = true;
            anim.SetBool("isShooting", true);
            changeGun(2);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Hider") {
            isHiding = false;
            toChase();
            anim.SetBool("isShooting", false);
        }
    } 


}
