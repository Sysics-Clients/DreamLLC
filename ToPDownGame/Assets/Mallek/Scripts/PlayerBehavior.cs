using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehavior : MonoBehaviour
{
    public PlayerPos playerPos;

    public delegate void Damege(float value);
    public Damege damege;

    public delegate void State(MovmentControler.State state);
    public State state;

    public delegate MovmentControler.State GetState();
    public GetState getState;

    public delegate void Die();
    public Die die;

    private void Start()
    {
        changePos(PlayerPos.Parking);
    }
    public void changePos(PlayerPos pos)
    {
        switch (pos)
        {
            case PlayerPos.Parking:
                transform.position = new Vector3(92, 1, 66);
                break;
            case PlayerPos.Kitchen1:

                break;
            case PlayerPos.Kitchen2:
                break;
            case PlayerPos.RoofTop:
                transform.position = new Vector3(41, 25, 97);
                break;

        }
    }
    public enum PlayerPos
    {
        Parking,
        RoofTop,
        Kitchen1,
        Kitchen2,
        Labo,
    }
}
