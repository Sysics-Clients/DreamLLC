using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPrefabItem : MonoBehaviour
{
    public Text priceText;
    public Image iItem;
    public string name;
    private int price;
    public GameObject frame;
    ItemObjects item;

    private void OnEnable()
    {
        GeneralEvents.select += inselect;
        GeneralEvents.activeItems += activeItem;
    }

    private void OnDisable()
    {
        GeneralEvents.select -= inselect;
        GeneralEvents.activeItems -= activeItem;
    }
    // Start is called before the first frame update
    void Start()
    {
        frame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
    public void setValues(ItemObjects item)
    {
        this.item = item;
        name = item.nameItem;
        iItem.sprite = item.spriteChoice;
        price = item.price;
        priceText.text = price + "$";
    }

    public void clicked()
    {
        GeneralEvents.select();
        frame.SetActive(true);
        GeneralEvents.showItem(item);
        GeneralEvents.buy = buy;
    } 

    void inselect()
    {
        frame.SetActive(false);
    }

    void buy()
    {
        ///////////////////////////////////////////////////////////////////
        print(name);
        item.state = StateItem.toUse;
    }

    public void activeItem(ItemTypes types)
    {
        if (types!=item.type)
        {
            this.gameObject.SetActive(false);
        }
        
    }
}
