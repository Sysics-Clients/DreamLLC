using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartItem : MonoBehaviour
{
    BoneCombiner boneCombiner;
    public ItemObjects itemBoots,itemLegs,itemChest;
    // Start is called before the first frame update
    void Start()
    {
        boneCombiner = new BoneCombiner(gameObject);
        boneCombiner.AddLimb(itemBoots.characterDisplay,itemBoots.boneNames);
        boneCombiner.AddLimb(itemLegs.characterDisplay, itemLegs.boneNames);
        boneCombiner.AddLimb(itemChest.characterDisplay, itemChest.boneNames);
    }

    // Update is called once per frame
    
}
