using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageBehavior : MonoBehaviour
{
    public delegate void ChangeMovement(HostageController.Movement move);
    public ChangeMovement changeMovement;
    
    public delegate void ChangeState(HostageStates.State state);
    public ChangeState changeState;
    
    public delegate void ChangeAnimationSpeed(float speed);
    public ChangeAnimationSpeed changeAnimationSpeed;


}
