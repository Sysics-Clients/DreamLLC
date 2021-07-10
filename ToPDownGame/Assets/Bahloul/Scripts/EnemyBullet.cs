using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject sender;
    public GameObject BloodEffect;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            //PlayerBehavior.instance.modifyHealth(sender.GetComponent<EnemyBehavior>().Item.damage);
            Instantiate(BloodEffect,collision.transform.position,Quaternion.Euler(transform.rotation.eulerAngles));
        }
        Destroy(gameObject);
    }
}
