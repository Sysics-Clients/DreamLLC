using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public enum ItemTypes
    {
        legs,
        AK,
        Pistol,
        knife,
        Boots,
        Chest,
        Casque,
        Shield
}

    public enum StateItem
    {
        toUse,
        current,
        toBuy
    }

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/item")]
    public class ItemObjects : ScriptableObject
    {
        public string nameItem;
        public Sprite spriteChoice;
        public GameObject prefab;
        public Material material;
        public ItemTypes type;
        public bool activeted, visible;
        public int price;
        [TextArea(15, 20)]
        public string description;
        public StateItem state;
       

        public List<string> boneNames = new List<string>();

        

        private void OnValidate()
        {
            boneNames.Clear();
            if (prefab == null)
                return;
            if (!prefab.GetComponent<SkinnedMeshRenderer>())
                return;

            var renderer = prefab.GetComponent<SkinnedMeshRenderer>();
            var bones = renderer.bones;

            foreach (var t in bones)
            {
                boneNames.Add(t.name);
            }

        }
    }







