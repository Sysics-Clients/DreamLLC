using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBehavior : MonoBehaviour
{
    public bool isVisible = true;
    public EnemyItem Item;
    public GameObject Player;
    public Canvas EnemyCanvas;
    public float TimeToDisappear = 3;

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

    public delegate float GetCurrentHealth();
    public GetCurrentHealth currentHealth;

    public delegate void TakeDamage(float damage);
    public TakeDamage takeDamage;
    public List<SkinnedMeshRenderer> skinnedMeshRenderers;
    public Animator thisAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isVisible == true && !thisAnimator.enabled)
        {
            thisAnimator.enabled = true;
            foreach (var item in skinnedMeshRenderers)
            {
                item.enabled = true;
            }
        }
        if (isVisible == false && thisAnimator.enabled)
        {
            thisAnimator.enabled = false;
            foreach (var item in skinnedMeshRenderers)
            {
                item.enabled = false;
            }
        }
    }
    private void dying()
    {
        StartCoroutine(WaitAndDie());
        isVisible = false;
    }
    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(TimeToDisappear);
        Destroy(gameObject);
    }
}
