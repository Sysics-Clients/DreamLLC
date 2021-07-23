using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform playerTransform;

    public delegate void EnemyMovement(EnemyController.Movement move);
    public EnemyMovement enemyMovement;

    public delegate void EnemyState(EnemyStates.State state);
    public EnemyState enemyState;

    public delegate EnemyStates.State GetCurrentState();
    public GetCurrentState getCurrentState;


    public delegate bool CanSeeThePlayer();
    public CanSeeThePlayer canSeeThePlayer;

    public delegate bool CheckLongRange(float radius,float angle);
    public CheckLongRange checkLongRange;

    public delegate float GetCurrentHealth();
    public GetCurrentHealth currentHealth;

    public delegate void TakeDamage(float damage);
    public TakeDamage takeDamage;

    public delegate void SetEnemyFovColor(Color color);
    public SetEnemyFovColor setEnemyFovColor;

    public delegate void DisableOrEnableFieldOfView(bool state);
    public DisableOrEnableFieldOfView disableOrEnableFieldOfView;


    public delegate GameObject GetBullet();
    public GetBullet getBullet;

    public delegate void ReturnBullet(GameObject bullet);
    public ReturnBullet returnBullet;

    public delegate void ToHide();
    public ToHide toHide;
  
    public EnemyItem Item;


    private void Start()
    {
        //StartCoroutine(FOVActivition());
    }
    /*private IEnumerator FOVActivition()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            print(Vector3.Distance(transform.position, playerTransform.position));
            if (Vector3.Distance(transform.position, playerTransform.position) < 10)
                disableOrEnableFieldOfView(true);
            else
                disableOrEnableFieldOfView(false);
        }
    }*/
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(20);
            print("take damage");
        }
    }

}
