using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{   private enum State
    {
        Idle,
        Roaming,
        Chasing,
        Attack,
        Death,
    }
    Coroutine WaitIdle;
    private State currentState;
    private int currentPos=0;
    [SerializeField]
    private float timeToWait;
    public List<Vector3> Positions = new List<Vector3>();
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        Positions.Add(transform.position);
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Idle;
        WaitIdle = StartCoroutine(WaitAndPrint());
    }
    
    // Update is called once per frame
    void Update()
    {   
        switch (currentState) {
            case State.Idle:
                
                //Debug.Log("Idle State");
                break;
            case State.Roaming:
                agent.SetDestination(Positions[0]);
                if (Vector3.Distance(transform.position, Positions[currentPos]) < 1f) {
                    //Debug.Log("Reach Destination");
                    WaitIdle = StartCoroutine(WaitAndPrint());
                    currentState = State.Idle;
                    
                }
                //Debug.Log("Roaming State");
                break;
            case State.Chasing:
                //Chasing The Player
                break;
            case State.Attack:
                //Attacking the Player
                break;
            case State.Death:
                //The Enemy Death
                break;
        }
        
    }
    private Vector3 GetPosition()
    {
        if (currentPos < (Positions.Count - 1))
        {
            
            currentPos++;
            Debug.Log(currentPos);
            return Positions[currentPos];
            
        }
        currentPos = 0;
        Debug.Log(currentPos);
        return Positions[0];


    }
    private IEnumerator WaitAndPrint()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToWait);
            GetPosition();
            currentState = State.Roaming;
        }
    }
}
