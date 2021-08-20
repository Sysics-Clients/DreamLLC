using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivation : MonoBehaviour
{
    public List<GameObject> FullEnemyList;
    public List<GameObject> SniperEnemyList;
    public List<GameObject> DroneEnemyList;

    public Transform playerTransform;

    [SerializeField]
    private float distanceFromPlayer;
    // Start is called before the first frame update
    private void Awake()
    {
        //For Full AI Enemies
        FullEnemyList = new List<GameObject>();
        GameObject[] gameObj = GameObject.FindGameObjectsWithTag("enemy");
        foreach(GameObject obj in gameObj)
        {
            FullEnemyList.Add(obj);
        }

        //For Sniper AI Enemies
        SniperEnemyList = new List<GameObject>();
         gameObj = GameObject.FindGameObjectsWithTag("Sniper");
        foreach (GameObject obj in gameObj)
        {
            SniperEnemyList.Add(obj);
        }

        //For Drone AI Enemies

        DroneEnemyList = new List<GameObject>();
        gameObj = GameObject.FindGameObjectsWithTag("Drone");
        foreach (GameObject obj in gameObj)
        {
            DroneEnemyList.Add(obj);
        }

        StartCoroutine(CheckActivation());
    }
    IEnumerator CheckActivation() {
        if (FullEnemyList.Count != 0)
        {
            foreach(GameObject obj in FullEnemyList)
            {
                if (obj != null)
                {
                    if (Vector3.Distance(playerTransform.position, obj.transform.position) > distanceFromPlayer)
                    {
                        obj.GetComponent<EnemyBehavior>().isVisible = false;
                    }
                    else
                    {
                        obj.GetComponent<EnemyBehavior>().isVisible = true;
                    }
                }
               
            }
        }
        //For Sniper AI Enemies
        if (SniperEnemyList.Count != 0)
        {
            foreach (GameObject obj in SniperEnemyList)
            {
                if (obj != null)
                {
                    if (Vector3.Distance(playerTransform.position, obj.transform.position) > distanceFromPlayer)
                    {
                        obj.GetComponent<SniperBehavior>().isVisible = false;
                    }
                    else
                    {
                        obj.GetComponent<SniperBehavior>().isVisible = true;
                    }
                }
               
            }
        }
        //For Drone AI Enemies
        if (DroneEnemyList.Count != 0)
        {
            foreach (GameObject obj in DroneEnemyList)
            {
                if (obj != null)
                {
                    if (Vector3.Distance(playerTransform.position, obj.transform.position) > distanceFromPlayer)
                    {
                        obj.GetComponent<DroneBehavior>().isVisible = false;
                    }
                    else
                    {
                        obj.GetComponent<DroneBehavior>().isVisible = true;
                    }
                }
                
            }
        }
            yield return new WaitForSeconds(0.1f);
        StartCoroutine(CheckActivation());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
