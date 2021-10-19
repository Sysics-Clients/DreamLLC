using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class itempTemplate : MonoBehaviour
{
    public Text price;
    public Image image;
    public bool active;

    public void itemTemplate (ItemObjects item)
    {
        image.sprite = item.spriteSelected;
        price.text = item.price+"$";
        active = item.activeted;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
