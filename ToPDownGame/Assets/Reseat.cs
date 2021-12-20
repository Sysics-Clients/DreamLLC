using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reseat : MonoBehaviour
{
    public ListItems listitemsPrefab;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in listitemsPrefab.items)
        {
            item.state = StateItem.toBuy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
