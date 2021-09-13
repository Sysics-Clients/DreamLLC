using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class BarrelExplosion : MonoBehaviour
{
    public GameObject ExplosionVfx;
    private int ShotNumber = 0;
    [SerializeField] float ShotMax;
    [SerializeField] float DistanceToDamage;
    [SerializeField] float damage;
    [SerializeField] float UpSpeed;
    [SerializeField] float ForwardSpeed;
    public LayerMask EnemyLayer;
    private GameObject player;
    private Rigidbody PlayerRb;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerRb = player.GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet"&&ShotNumber<= ShotMax)
        {
            if (ShotNumber == ShotMax)
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
                    rangeChecks[i].gameObject.GetComponent<NavMeshAgent>().enabled = false;
                }
                else if (rangeChecks[i].transform.tag == "Sniper")
                {
                    rangeChecks[i].gameObject.GetComponent<SniperBehavior>().takeDamage(damage);
                    rangeChecks[i].gameObject.GetComponent<NavMeshAgent>().enabled = false;
                }
                else if(rangeChecks[i].transform.tag == "Drone")
                {
                    DroneBehavior  droneBehavior = rangeChecks[i].gameObject.GetComponent<DroneBehavior>();
                    Rigidbody  droneRb = rangeChecks[i].gameObject.GetComponent<Rigidbody>();
                    droneBehavior.disableOrEnableRenderingFov(false);
                    droneBehavior.Death();
                    droneRb.isKinematic = false;
                    droneRb.AddForce(Vector3.up * Random.Range(50,100));
                    droneRb.AddForce(Vector3.forward * Random.Range(50, 100));
                     rangeChecks[i].transform.DOShakeRotation(1, 50, 20, 5);
                     //rangeChecks[i].transform.DOShakePosition(1, 1, 1, 1);



                }
                /*rangeChecks[i].transform.DOShakeRotation(0.5f, 5, 1, 5);
                rangeChecks[i].transform.DOShakePosition(1, 10,12, 12);*/
                Rigidbody rb = rangeChecks[i].GetComponent<Rigidbody>();
                rb.isKinematic = false;
                // rb.AddForce(Vector3.up * UpSpeed);
                // rb.AddForce(Vector3.forward * ForwardSpeed);
                rb.AddExplosionForce(700, transform.position, 5);
            }

        }
        if (Vector3.Distance(player.transform.position, transform.position) < DistanceToDamage)
        {
            player.gameObject.GetComponent<PlayerBehavior>().damege(damage);
            PlayerRb.isKinematic = false;
            player.GetComponent<CharacterController>().enabled = false;
            PlayerRb.AddForce(Vector3.up * UpSpeed,ForceMode.Force);
            PlayerRb.AddForce(Vector3.forward * ForwardSpeed,ForceMode.Force);
            
            /*PlayerRb.isKinematic = false;
             PlayerRb.AddForce(Vector3.up * UpSpeed);
             PlayerRb.AddForce(Vector3.forward * ForwardSpeed);*/

            /*player.transform.DOShakeRotation(0.5f, 5, 1, 5);
            player.transform.DOShakePosition(1, 10, 12, 12);*/

        }
    }

}
