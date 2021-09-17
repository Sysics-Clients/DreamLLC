using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    public Transform iPad;
    public Transform PLayer;
    private Quaternion newPos;
    // Start is called before the first frame update
    
    void  Start()
    {
        StartCoroutine(CheckDirection());
       
    }
    private void Update()
    {
       
    }
    IEnumerator CheckDirection()
    {
        yield return new WaitForSeconds(0.2f);
        if (iPad != null)
        {
            /*newPos = Quaternion.FromToRotation(transform.rotation.eulerAngles, ( iPad.position - PLayer.position).normalized);
            transform.rotation = Quaternion.Euler(90, newPos.eulerAngles.y, 0);*/
            /*Vector3 root = Vector3.Lerp(transform.position, iPad.position, 3);
            transform.forward = root;
            transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, 0);*/


            transform.LookAt(iPad.position);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y , 0);

            //Vector3 dir =   PLayer.position - iPad.position;
            //float ang = Mathf.Atan2(dir.y, dir.x);
            //ang = 180 * ang / Mathf.PI;
            //ang = Mathf.Rad2Deg * ang;
            /* float ang = Vector3.Angle(transform.position, iPad.position);
             ang = 180 * ang / Mathf.PI;*/
            //print(ang);

            
            
            //transform.rotation = Quaternion.Euler(90, ang, 0); 

            if (Vector3.Distance(PLayer.position, iPad.position) < 10)
            {
                transform.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
            else
            {
                transform.GetComponentInChildren<SpriteRenderer>().enabled = true;
            }
        }
        StartCoroutine(CheckDirection());
    }
}
