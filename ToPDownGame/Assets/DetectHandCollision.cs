using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHandCollision : MonoBehaviour
{
    public ZombieBehavior zombieBehavior;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(other!=null)
            other.gameObject.GetComponent<PlayerBehavior>().damege((int)zombieBehavior.CurrentDamage);
        }
    }
}
