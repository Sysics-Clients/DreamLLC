using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehavior : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public delegate void ChangeSpeed(float speed);
    public ChangeSpeed changeSpeed;

    public delegate void ChangeState(DroneStates.State state);
    public ChangeState changeState;

    public delegate DroneStates.State GetDroneState();
    public GetDroneState getDroneState;
    
    public delegate bool CanSeeThePlayer();
    public CanSeeThePlayer canSeeThePlayer;

    public delegate void SetDroneFovColor(Color color);
    public SetDroneFovColor setDroneFovColor;

    public delegate void DisableOrEnableFieldOfView(bool state);
    public DisableOrEnableFieldOfView disableOrEnableFieldOfView;

    public delegate void DisableOrEnableRenderingFov(bool state);
    public DisableOrEnableRenderingFov disableOrEnableRenderingFov;
    
    public delegate void CallForHelp();
    public CallForHelp callForHelp;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckPlayerFOV());
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private IEnumerator CheckPlayerFOV()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
            yield return wait;
            if (canSeeThePlayer())
            {
                setDroneFovColor(Color.red);
                
            if (getDroneState() != DroneStates.State.Chasing)
                    {
                        changeState(DroneStates.State.Chasing);
                        callForHelp();
                        changeSpeed(runSpeed);
                    }
            }
            else 
            {
                if ((getDroneState() == DroneStates.State.Chasing))
                 setDroneFovColor(Color.yellow);
            }
            RaycastHit hit;
            if(Physics.Raycast(transform.position, -1*transform.up, out hit))
            {
                if (hit.collider.tag == "Ground")
                {
                    transform.GetChild(0).position= new Vector3(transform.position.x, hit.collider.transform.position.y + 0.2f, transform.position.z); 
                }
            }
            StartCoroutine(CheckPlayerFOV());
    }
}
