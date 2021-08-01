using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivation : MonoBehaviour
{
    public List<GameObject> ObjectList;

    public Transform playerTransform;

    [SerializeField]
    private float distanceFromPlayer;
    // Start is called before the first frame update
    private void Awake()
    {
        ObjectList = new List<GameObject>();
        GameObject[] gameObj = GameObject.FindGameObjectsWithTag("enemy");
        foreach(GameObject obj in gameObj)
        {
            ObjectList.Add(obj);
        }
        StartCoroutine(CheckActivation());
    }
    IEnumerator CheckActivation() {
        if (ObjectList.Count != 0)
        {
            foreach(GameObject obj in ObjectList)
            {
                if (Vector3.Distance(playerTransform.position, obj.transform.position) > distanceFromPlayer)
                {
                    obj.SetActive(false);
                }
                else
                {
                    obj.SetActive(true);
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
