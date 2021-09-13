using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BarrelExplosion : MonoBehaviour
{
    public GameObject ExplosionVfx;
    [SerializeField] int ShotNumber = 0;
    [SerializeField] float DistanceToDamage;
    [SerializeField] float damage;
    public LayerMask EnemyLayer;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet"&&ShotNumber<=1)
        {
            if (ShotNumber == 1)
            {
                ExplosionVfx.SetActive(true);
                ExplosionVfx.GetComponent<ParticleSystem>().Play();
                ExplosionVfx.GetComponent<AudioSource>().Play();
                GetComponent<MeshRenderer>().enabled = false;
                StartCoroutine(waitToDestroy());
                checkDistance();
            }
            ShotNumber++;
        }
    }
    IEnumerator waitToDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
    void checkDistance()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, DistanceToDamage, EnemyLayer);

        if (rangeChecks.Length != 0)
        {
            for (int i = 0; i < rangeChecks.Length; i++)
            {
                if (rangeChecks[i].transform.tag == "enemy")
                {
                    rangeChecks[i].gameObject.GetComponent<EnemyBehavior>().takeDamage(damage);
                }
                else if (rangeChecks[i].transform.tag == "Sniper")
                {
                    rangeChecks[i].gameObject.GetComponent<SniperBehavior>().takeDamage(damage);
                }
                rangeChecks[i].transform.DOShakeRotation(0.5f, 5, 1, 5);
                rangeChecks[i].transform.DOShakePosition(0.5f, 0.5f, 1, 5);
            }

        }
        if (Vector3.Distance(player.transform.position, transform.position) < DistanceToDamage)
        {
            player.gameObject.GetComponent<PlayerBehavior>().damege(damage);
            player.transform.DOShakeRotation(0.5f, 5, 1, 5);
            player.transform.DOShakePosition(0.5f, 0.5f, 1, 5);
        }
    }

}
