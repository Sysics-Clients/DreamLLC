using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform gun;
    public float range=20;
    public int damege = 20;
    public PlayerBehavior playerBehavior;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            shoot();
        }
    }

    public void shoot()
    {
        if(playerBehavior!=null)
            playerBehavior.state(MovmentControler.State.attack);
        
    }
    public void shot()
    {
        print("hhhh");
        RaycastHit raycastHit;
        if (Physics.Raycast(gun.position, gun.forward, out raycastHit, range))
        {
            //Eney enemy=hit.transform.GetComponent<Enemy>();
            //
            
        }
    }
}
