using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamicweaponslist : MonoBehaviour
{
    // Start is called before the first frame update
    public ListItems listitems;
    public Transform listitemsHolder;

   // public Transform listitemsHolder1;

   //public Transform listitemsHolder2;
    public GameObject item;

    void Instantiate()
    {


        for (int i = 0; i < listitems.items.Count; i++)
        {
           
            GameObject go = Instantiate(item, listitemsHolder);
            go.GetComponent<itempTemplate>().itemTemplate(listitems.items[i]);
         
           
        }

    }
   void Start()
    {
        Instantiate();
    }
    
}
