﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartPrefabItem : MonoBehaviour
{
    public Text priceText;
    public Image iItem;
    public string name;
    private int price;
    public GameObject frame;
    public ItemObjects item;

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
        
        if (item.state == StateItem.current)
        {
            if (item.type == ItemTypes.AK || item.type == ItemTypes.knife || item.type == ItemTypes.Pistol)
            {
                GeneralEvents.isCurrentWeapon();
                
            }
            else
            {
                GeneralEvents.isCurrentClow();
                
            }
            
        }
        else if (item.state == StateItem.toBuy)
        {
            if (item.type == ItemTypes.AK || item.type == ItemTypes.knife || item.type == ItemTypes.Pistol)
            {
                GeneralEvents.buyWeapon = buy;
                GeneralEvents.toBuyWeapon();
            }
            else
            {
                GeneralEvents.buyClowths = buy;
                GeneralEvents.toBuyClow();
            }
            // GeneralEvents.toBuy();
        }
        else
        {
            if (item.type == ItemTypes.AK || item.type == ItemTypes.knife || item.type == ItemTypes.Pistol)
            {
                GeneralEvents.toUseWeapon();
                
            }
            else
            {
                
                GeneralEvents.toUseClow();
            }
            GeneralEvents.useIte = use;
            //GeneralEvents.toUse();
        }
    } 

    void inselect()
    {
        frame.SetActive(false);
    }

    void buy()
    {
        ///////////////////////////////////////////////////////////////////
        print(name);
        if (item.price<Singleton.coins)
        {
            Singleton.coins -= item.price;
            item.state = StateItem.toUse;
            clicked();
        }
        else
        {
            print("ma chrech");
        }
        print(Singleton.coins);
    }

    public void activeItem(ItemTypes types)
    {
        if (types!=item.type)
        {
            this.gameObject.SetActive(false);
        }
        
    }
    void use()
    {
        GeneralEvents.btnUseIte(item);
        clicked();
        print(name);
    }
}
