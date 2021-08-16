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
        
    }
    private void OnDisable()
    {
        droneBehavior.changeState -= changeState;
        droneBehavior.changeSpeed -= changeSpeed;
        droneBehavior.getDroneState -= getCurrentState;
        droneBehavior.callForHelp -= callForHelp;
    }
    public enum State
    {
        Idle,
        Roaming,
        Chasing,
        Helping,
    }
    public void changeSpeed(float speed) { Speed = speed; }
    public DroneStates.State getCurrentState() { return currentState; }
    public void changeState(State s) { currentState = s; }
    // Start is called before the first frame update
    void Start()
    {
        Speed = droneBehavior.walkSpeed;
        Positions.Add(transform);
        currentState = State.Idle;
        StartCoroutine(WaitOnIdle());
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
                if (Vector3.Distance(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), transform.position) > 1)
                    transform.position = Vector3.Lerp(transform.position, new Vector3(playerTransform.position.x, 5+ playerTransform.position.y, playerTransform.position.z), Time.deltaTime );
                if(Vector3.Distance(new Vector3(playerTransform.position.x,transform.position.y, playerTransform.position.z), transform.position)>25)
                {
                    changeState(State.Idle);
                    WaitIdle=StartCoroutine(WaitOnIdle());
                    Speed = droneBehavior.walkSpeed;
                }
                transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));

                break;
            case State.Helping:

                break;
        }
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
    public void toHelp(Vector3 pos)
    {
        if(WaitIdle!=null)
            StopCoroutine(WaitIdle);
        posToGoHelp = pos;
        changeState(State.Helping);
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
                            if(rangeChecks[i].GetComponent<EnemyBehavior>().getCurrentState()!=EnemyStates.State.Attack)
                                rangeChecks[i].GetComponent<EnemyStates>().toHelp(transform.position);
                            break;
                        case "Sniper":
                            if (rangeChecks[i].GetComponent<SniperBehavior>().getState() != SniperStates.State.Attack)
                                rangeChecks[i].GetComponent<SniperStates>().toHelp(transform.position);
                            break;
                        case "Drone":
                            if (rangeChecks[i].GetComponent<DroneBehavior>().getDroneState() != DroneStates.State.Chasing)
                                rangeChecks[i].GetComponent<DroneStates>().toHelp(transform.position);
                            break;
                    }
                }
            }
        }
    }
}