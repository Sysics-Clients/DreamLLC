﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartItem : MonoBehaviour
{
    BoneCombiner boneCombiner;
    public ItemObjects itemBoots,itemLegs,itemChest,itemShield,itemCasque;
    private Transform transformBoots, transformLegs, transformChest;
    public Transform casque, shield;
    public GameObject hair,casqueObj,shieldObj;
    private void OnEnable()
    {
        GeneralEvents.setItem += setItem;
    }
    private void OnDisable()
    {
        GeneralEvents.setItem -= setItem;
    }
    // Start is called before the first frame update
    void Start()
    {
        boneCombiner = new BoneCombiner(gameObject);
        transformBoots= boneCombiner.AddLimb(itemBoots.prefab,itemBoots.boneNames);
        if (itemBoots.material!=null)
        {
            transformBoots.GetComponent<SkinnedMeshRenderer>().material = itemBoots.material;
        }
  
        transformLegs=boneCombiner.AddLimb(itemLegs.prefab, itemLegs.boneNames);
        if (itemLegs.material != null)
        {
            transformLegs.GetComponent<SkinnedMeshRenderer>().material = itemLegs.material;
        }
        
        transformChest =boneCombiner.AddLimb(itemChest.prefab, itemChest.boneNames);
        if (itemChest.material != null)
        {
            transformChest.GetComponent<SkinnedMeshRenderer>().material = itemChest.material;
        }
        if (itemShield.prefab!=null)
        {
            shieldObj= Instantiate(itemShield.prefab, shield);
        }
        if (itemCasque.prefab != null)
        {
            hair.SetActive(false);
            casqueObj=Instantiate(itemCasque.prefab, casque);
        }
    }
    
    public void setItem(ItemObjects item)
    {
        switch (item.type)
        {
            case ItemTypes.legs:
                Destroy(transformLegs.gameObject);
                transformLegs=boneCombiner.AddLimb(item.prefab, item.boneNames);
                itemLegs = item;
                if (item.material != null)
                {
                    transformLegs.GetComponent<SkinnedMeshRenderer>().material = item.material;
                }
                break;
            case ItemTypes.Boots:
                Destroy(transformBoots.gameObject);
                transformBoots=boneCombiner.AddLimb(item.prefab, item.boneNames);
                itemBoots = item;
                if (item.material != null)
                {
                    transformBoots.GetComponent<SkinnedMeshRenderer>().material = item.material;
                }
                break;
            case ItemTypes.Chest:
                Destroy(transformChest.gameObject);
                transformChest=boneCombiner.AddLimb(item.prefab, item.boneNames);
                itemChest = item;
                if (item.material != null)
                {
                    transformChest.GetComponent<SkinnedMeshRenderer>().material = item.material;
                }
                break;
            case ItemTypes.Casque:
                Destroy(casqueObj);
                if (item.prefab != null)
                {
                    hair.SetActive(false);
                    casqueObj = Instantiate(item.prefab, casque);
                }
                else
                {
                    hair.SetActive(true);
                }
                itemCasque = item;
                break;
            case ItemTypes.Shield:
                Destroy(shieldObj);
                if (itemShield.prefab != null)
                {
                    shieldObj = Instantiate(item.prefab, shield);
                }
                itemShield = item;
                break;
            default:
                break;
        }
        
        
        
    }
}
