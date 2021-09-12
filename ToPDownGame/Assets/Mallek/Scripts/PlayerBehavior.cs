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
    private void OnEnable()
    {
        
    }
    private void Start()
    {
        changePos(PlayerPos.Parking);
        print(transform.position);
    }
    private void Update()
    {

        if (Input.GetKey(KeyCode.M)){
            transform.position = GameObject.Find("Pad").transform.position;
        }
       
    }
    public void changePos(PlayerPos pos)
    {
        switch (pos)
        {
            case PlayerPos.Parking:
                transform.position = GameObject.Find("Pad").transform.position;   //new Vector3(92, 1, 66);
                break;
            case PlayerPos.Kitchen1:
                transform.position = new Vector3(-4, 10, -23);
                break;
            case PlayerPos.Labo:
                break;
            case PlayerPos.RoofTop:
                transform.position = new Vector3(41, 25, 97);
                break;

        }
        StartCoroutine(onStart());
    }
    public enum PlayerPos
    {
        Parking,
        RoofTop,
        Kitchen1,
        Kitchen2,
        Labo,
    }
    IEnumerator onStart()
    {
        yield return new WaitForSeconds(.5f);
        GetComponent<MovmentControler>().enabled = true;
    }
}
