using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemTypes
    {
        legs,
        AK,
        Pistol,
        knife,
        Boots,
        Chest,
        Shield,
        Casque
    }

public enum StateItem
{
    toBuy,
    toUse,
    Used
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/item")]
    public class ItemObjects : ScriptableObject
    {
        public string nameItem;
        public Sprite sprite;
        public GameObject prefab;
        public ItemTypes type;
        public bool activeted;
        public int price;
        public StateItem state;
        public bool visible;
        [TextArea(15, 20)]
        public string description;
        public Material material;
        
        
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
        
        public void setMaturial() {
            if (material != null)
            {
                prefab.GetComponent<SkinnedMeshRenderer>().materials[0] = material;
            }
        }
    }
[CreateAssetMenu(fileName = "Items", menuName = "Inventory System/Items/list item")]
public class ListItems : ScriptableObject
{
    public List<ItemObjects> items;
}






