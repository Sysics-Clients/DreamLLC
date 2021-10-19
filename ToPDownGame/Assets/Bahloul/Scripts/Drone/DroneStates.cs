using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneStates : MonoBehaviour
{
    public DroneBehavior droneBehavior;
    private float Speed;
    private State currentState;
    public Transform playerTransform;
    private Vector3 LastPlayerPosition;
    public List<Transform> Positions;
    public float timeToWaitIdle;
    int currentPos;
    Coroutine WaitIdle;
    public LayerMask EnemyLayer;
    private Vector3 posToGoHelp;

    private void OnEnable()
    {
        droneBehavior.changeState += changeState;
        droneBehavior.changeSpeed += changeSpeed;
        droneBehavior.getDroneState += getCurrentState;
        droneBehavior.callForHelp += callForHelp;
        droneBehavior.toIdle += toIdle;
        
    }
    private void OnDisable()
    {
        droneBehavior.changeState -= changeState;
        droneBehavior.changeSpeed -= changeSpeed;
        droneBehavior.getDroneState -= getCurrentState;
        droneBehavior.callForHelp -= callForHelp;
        droneBehavior.toIdle -= toIdle;
    }
    public enum State
    {
        Idle,
        Roaming,
        Chasing,
        Helping,
        Death,
    }
    public void changeSpeed(float speed) { Speed = speed; }
    public DroneStates.State getCurrentState() { return currentState; }
    public void changeState(State s) { 
        currentState = s;
        if (s == State.Chasing)
        {
            if (WaitIdle != null)
                StopCoroutine(WaitIdle);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Speed = droneBehavior.walkSpeed;
        Positions.Add(transform);
        currentState = State.Idle;
        WaitIdle = StartCoroutine(WaitOnIdle());
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!droneBehavior.isVisible)
            return;
        switch (currentState)
        {
           case State.Idle:
                break;
            case State.Roaming:
                transform.position = Vector3.Lerp(transform.position, new Vector3(Positions[currentPos].position.x, transform.position.y, Positions[currentPos].position.z), Time.deltaTime*Speed);                if (Vector3.Distance(new Vector3(transform.position.x,0, transform.position.z), new Vector3(Positions[currentPos].position.x, 0, Positions[currentPos].position.z) ) < 0.2f) //Reach Destination
                {
                    toIdle();
                }
                transform.LookAt(new Vector3( Positions[currentPos].position.x,transform.position.y, Positions[currentPos].position.z));
                break;
            case State.Chasing:
                float dist = Vector3.Distance(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), transform.position);
                if (dist > 10)
                {
                    changeState(State.Idle);
                    WaitIdle = StartCoroutine(WaitOnIdle());
                    Speed = droneBehavior.walkSpeed;
                }
                else if (dist > 6)
                {
                    Speed = droneBehavior.walkSpeed;
                    transform.position = Vector3.Lerp(transform.position, new Vector3(playerTransform.position.x, transform.position.y , playerTransform.position.z), Time.deltaTime * Speed);

                }
                else if (dist > 1)
                {
                    Speed = droneBehavior.runSpeed;
                }
                transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
                transform.position = Vector3.Lerp(transform.position, new Vector3(playerTransform.position.x, transform.position.y , playerTransform.position.z), Time.deltaTime * Speed);


                break;
            case State.Helping:

                break;
            case State.Death:

                break;
        }
    }
    public void toHelp(Vector3 pos)
    {
        if (WaitIdle != null)
            StopCoroutine(WaitIdle);
        posToGoHelp = pos;
        changeState(State.Chasing);
        droneBehavior.setDroneFovColor(Color.yellow);
    }
    private IEnumerator WaitOnIdle()
    {
        yield return new WaitForSeconds(timeToWaitIdle);
        setPosition();
        toRoaming();
    }

    void toIdle()
    {
        currentState = State.Idle;
        WaitIdle = StartCoroutine(WaitOnIdle());
    }
    void toRoaming()
    {
        currentState = State.Roaming;
    }
    private void setPosition()
    {
        if (currentPos < (Positions.Count - 1))
            currentPos++;
        else
            currentPos = 0;
    }
    
    public  void callForHelp()
    {
        
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, 30, EnemyLayer);
        if (rangeChecks.Length !=0)
        {
            for (int i = 0; i < rangeChecks.Length; i++)
            {
                if ((rangeChecks[i].gameObject != gameObject) )
                {
                    //(Vector3.Distance(transform.position, rangeChecks[i].transform.position) > 10)
                    switch (rangeChecks[i].tag)
                    {
                        case "enemy":
                            EnemyBehavior enemyBehavior = rangeChecks[i].GetComponent<EnemyBehavior>();
                            EnemyStates enemyStates = rangeChecks[i].GetComponent<EnemyStates>();
                            if ((enemyBehavior != null) && (enemyBehavior.isVisible)&&enemyStates!=null)
                                if (enemyBehavior.getCurrentState() != EnemyStates.State.Attack)
                                    enemyStates.toHelp(playerTransform.position);
                            break;
                        case "Sniper":
                            SniperBehavior sniperBehavior = rangeChecks[i].GetComponent<SniperBehavior>();
                            SniperStates sniperStates = rangeChecks[i].GetComponent<SniperStates>();
                            if ((sniperBehavior != null) && (sniperBehavior.isVisible)&&sniperStates!=null)
                                if (sniperBehavior.getState() != SniperStates.State.Attack)
                                    sniperStates.toHelp(playerTransform.position);
                            break;
                        case "Drone":
                            DroneBehavior droneBehavior = rangeChecks[i].GetComponent<DroneBehavior>();
                            DroneStates droneStates = rangeChecks[i].GetComponent<DroneStates>();

                            if ((droneBehavior != null) && (droneBehavior.isVisible)&&droneStates!=null)
                                if (droneBehavior.getDroneState() != DroneStates.State.Chasing)
                                {
                                    droneStates.toHelp(playerTransform.position);
                                }
                            break;
                    }
                }
            }
        }
    }
}