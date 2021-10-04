using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dynamicweaponslist : MonoBehaviour
{
    // Start is called before the first frame update
    public ListItems listitemsPrefab;
    public Transform contentPistol, contentAK, contentknife, contentShoes, contentBot, contentTop;
    public GameObject prefab;

    public Image ISelect;
    public Text weaponSelect,damege,speed;
    


    private void OnEnable()
    {
        GeneralEvents.showItem += show;
    }

    private void OnDisable()
    {
        GeneralEvents.showItem -= show;
    }
    void Start() {
        
        foreach (ItemObjects item in listitemsPrefab.items)
        {
            if (item.visible)
            {
                
                switch (item.type)
                {
                    case ItemTypes.legs:
                        Instantiate(prefab, contentBot).GetComponent<StartPrefabItem>().setValues(item);
                        break;
                    case ItemTypes.AK:
                        Instantiate(prefab, contentAK).GetComponent<StartPrefabItem>().setValues(item);
                        break;
                    case ItemTypes.Pistol:
                        Instantiate(prefab, contentPistol).GetComponent<StartPrefabItem>().setValues(item);
                        break;
                    case ItemTypes.knife:
                        Instantiate(prefab, contentknife).GetComponent<StartPrefabItem>().setValues(item);
                        break;
                    case ItemTypes.Boots:
                        Instantiate(prefab, contentShoes).GetComponent<StartPrefabItem>().setValues(item);
                        break;
                    case ItemTypes.Chest:
                        Instantiate(prefab, contentTop).GetComponent<StartPrefabItem>().setValues(item);
                        break;
                    default:
                        break;
                }
            }
            
        }
    
    }
    void show(ItemObjects item)
    {
        
        if (item.GetType().Equals(typeof(WeaponItem)))
        {
            ISelect.sprite = item.sprite;
            weaponSelect.text = item.nameItem;
        }
        else
        {
            GeneralEvents.setItem(item);
        }
        
        
    }

    public void buy()
    {
        GeneralEvents.buy();
    }
}
