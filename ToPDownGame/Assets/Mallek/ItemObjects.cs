﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public enum ItemTypes
    {
        legs,
        Weapon,
        Boots,
        Chest,
    }

   
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/item")]
    public class ItemObjects : ScriptableObject
    {

        public Sprite uiDisplay;
        public GameObject characterDisplay;
        public ItemTypes type;
        [TextArea(15, 20)]
        public string description;
       

        public List<string> boneNames = new List<string>();

        

        private void OnValidate()
        {
            boneNames.Clear();
            if (characterDisplay == null)
                return;
            if (!characterDisplay.GetComponent<SkinnedMeshRenderer>())
                return;

            var renderer = characterDisplay.GetComponent<SkinnedMeshRenderer>();
            var bones = renderer.bones;

            foreach (var t in bones)
            {
                boneNames.Add(t.name);
            }

        }
    }

    

    
    