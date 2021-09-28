using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamicweaponslist : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject listitemsPrefab;
    public Transform listitemsHolder;
    public int WeaponsNbr;
    void Start() { 
    for(int i=0; i< WeaponsNbr; i++)
        {
            Instantiate(listitemsPrefab, listitemsHolder);
        }
    }

}
