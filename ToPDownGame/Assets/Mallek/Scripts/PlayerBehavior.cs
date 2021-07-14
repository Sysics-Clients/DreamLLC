using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehavior : MonoBehaviour
{

    public delegate void Damege(float value);
    public Damege damege;

    public delegate void State(MovmentControler.State state);
    public State state;
}
