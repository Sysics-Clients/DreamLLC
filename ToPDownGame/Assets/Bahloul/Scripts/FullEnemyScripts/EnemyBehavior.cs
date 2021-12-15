using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    public enum Scenarios
    {
        FullAiEnemy,
        Sniper,

    }
    public bool isVisible = true;
    public float gunBloom=0;
    public GameObject player;
    public Canvas EnemyCanvas;
    public float TimeToDisappear = 3;
    public GameObject AccessCard;

    public delegate void ChangeGunBloom(float _gunBloom);
    public ChangeGunBloom changeGunBloom;

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
    
    public delegate void DisableOrEnableRenderingFov(bool state);
    public DisableOrEnableRenderingFov disableOrEnableRenderingFov;

    public delegate GameObject GetBullet();
    public GetBullet getBullet;

    public delegate void ReturnBullet(GameObject bullet);
    public ReturnBullet returnBullet;

    public delegate void ToHide();
    public ToHide toHide;
  
    public EnemyItem Item;
    public List<SkinnedMeshRenderer> skinnedMeshRenderers;
    public Animator thisAnimator;

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
        }
        if (isVisible==true&&!thisAnimator.enabled)
        {
            thisAnimator.enabled = true;
            foreach (var item in skinnedMeshRenderers)
            {
                item.enabled = true;
            }
        }
        if (isVisible==false&&thisAnimator.enabled)
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
    }
    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(TimeToDisappear);
        Destroy(gameObject);
    }

}
