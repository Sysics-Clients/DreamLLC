using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public delegate void EnemyMovement(EnemyController.Movement move);
    public EnemyMovement enemyMovement;

    public delegate void EnemyState(EnemyStates.State state);
    public EnemyState enemyState;


    public delegate bool FieldOfView();
    public FieldOfView fieldOfView;

    public delegate float GetCurrentHealth();
    public GetCurrentHealth currentHealth;

    public delegate void TakeDamage(float damage);
    public TakeDamage takeDamage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            takeDamage(20);
            print("take damage");
        }
    }
    public EnemyItem Item;
    
}
