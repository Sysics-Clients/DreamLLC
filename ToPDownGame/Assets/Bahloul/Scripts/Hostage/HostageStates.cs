using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HostageStates : MonoBehaviour
{
    NavMeshAgent agent;
    Transform playerTransform;
    public HostageBehavior hostageBehavior;
    private State currentState;
    public float stopDistanceMin = 5;
    public float stopDistanceMax = 15;
    public float distanceToStand = 7;
    private void OnEnable()
    {
        hostageBehavior.changeState += changeState;
    }
    private void OnDisable()
    {
        hostageBehavior.changeState -= changeState;
    }
    public enum State
    {
        Sleeping,
        lost,
        Chasing,
        idle,
        standingUp,
    }
    public HostageStates.State getCurrentState() { return currentState; }
    public void changeState(State s) { currentState = s; }
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        transform.LookAt(playerTransform);
    }
    float distance;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GeneralEvents.checkMissionCompletion(MissionName.destroyEnemy,1))
            return;
        print(currentState);
        switch (currentState)
        {
            case State.Sleeping:
                distance = Vector3.Distance(transform.position, playerTransform.position);
                if (distance < distanceToStand)
                {
                    GeneralEvents.onTaskFinish(GetComponent<MissionObjects>().missionName, GetComponent<MissionObjects>().id);
                    hostageBehavior.changeMovement(HostageController.Movement.standUp);
                    changeState(State.standingUp);
                }
                break;
            case State.standingUp:
                    break;
            case State.Chasing:
                hostageBehavior.changeAnimationSpeed(3);
                agent.isStopped = false;
                agent.SetDestination(playerTransform.position);
                distance = Vector3.Distance(transform.position, playerTransform.position);
                if ((distance < stopDistanceMin) || (distance > stopDistanceMax))
                {
                    hostageBehavior.changeMovement(HostageController.Movement.idle);
                    changeState(State.idle);
                }
                break;
            case State.idle:
                hostageBehavior.changeAnimationSpeed(1);
                agent.isStopped = true;
                transform.LookAt(playerTransform);
                distance = Vector3.Distance(transform.position, playerTransform.position);
                if ((distance > stopDistanceMin) && (distance < stopDistanceMax))
                {
                    hostageBehavior.changeMovement(HostageController.Movement.Walk);
                    changeState(State.Chasing);
                }
                break;
            case State.lost:
                break;
        }
    }
    public void ToWalk()
    {
            hostageBehavior.changeMovement(HostageController.Movement.idle);
            changeState(State.idle);
    }
}
