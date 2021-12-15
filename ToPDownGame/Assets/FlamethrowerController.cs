using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerController : MonoBehaviour
{
    public int damage;
    
 void OnCollisionStay(Collision collision)
    {
       if (collision.gameObject.tag == "Player")

        {
                collision.gameObject.GetComponent<PlayerBehavior>().damege(damage);
        }
    }
}
