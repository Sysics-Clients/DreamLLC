using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBehavior : MonoBehaviour
{
    public EnemyItem Item;
    public Transform playerTransform;

    public delegate void EnemyMovement(SniperMovement.Movement move);
    public EnemyMovement enemyMovement;
    

    public delegate GameObject GetBullet();
    public GetBullet getBullet;

    public delegate void ReturnBullet(GameObject bullet);
    public ReturnBullet returnBullet;

    public delegate void ChangeGun(int i);
    public ChangeGun changeGun;


    public delegate void ChangeState(SniperStates.State state);
    public ChangeState changeState;

    public delegate SniperStates.State GetState();
    public GetState getState;

    public delegate void ToIdle();
    public ToIdle toIdle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
