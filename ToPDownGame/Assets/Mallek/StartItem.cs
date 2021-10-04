using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartItem : MonoBehaviour
{
    BoneCombiner boneCombiner;
    public ItemObjects itemBoots,itemLegs,itemChest;
    public Transform transformBoots, transformLegs, transformChest;
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
        
    }
    
    public void setItem(ItemObjects item)
    {
        switch (item.type)
        {
            case ItemTypes.legs:
                Destroy(transformLegs.gameObject);
                transformLegs=boneCombiner.AddLimb(item.prefab, item.boneNames);
                if (item.material != null)
                {
                    transformLegs.GetComponent<SkinnedMeshRenderer>().material = item.material;
                }
                break;
            case ItemTypes.Boots:
                Destroy(transformBoots.gameObject);
                transformBoots=boneCombiner.AddLimb(item.prefab, item.boneNames);
                if (item.material != null)
                {
                    transformBoots.GetComponent<SkinnedMeshRenderer>().material = item.material;
                }
                break;
            case ItemTypes.Chest:
                Destroy(transformChest.gameObject);
                transformChest=boneCombiner.AddLimb(item.prefab, item.boneNames);
                if (item.material != null)
                {
                    transformChest.GetComponent<SkinnedMeshRenderer>().material = item.material;
                }
                break;
            default:
                break;
        }
        
        
        
    }
}
