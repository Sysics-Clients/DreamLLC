using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dynamicweaponslist : MonoBehaviour
{
    // Start is called before the first frame update
    public ListItems listitemsPrefab;
    public Transform contentWeapon, contentClothes;
    public GameObject prefab;
    public List<GameObject> items;
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
                        items.Add(Instantiate(prefab, contentClothes));
                        items[items.Count - 1].GetComponent<StartPrefabItem>().setValues(item);
                        break;
                    case ItemTypes.AK:
                        items.Add(Instantiate(prefab, contentWeapon));
                        items[items.Count - 1].GetComponent<StartPrefabItem>().setValues(item);
                        
                        break;
                    case ItemTypes.Pistol:
                        items.Add(Instantiate(prefab, contentWeapon));
                        items[items.Count - 1].GetComponent<StartPrefabItem>().setValues(item);
                        break;
                    case ItemTypes.knife:
                        items.Add(Instantiate(prefab, contentWeapon));
                        items[items.Count - 1].GetComponent<StartPrefabItem>().setValues(item);
                        
                        break;
                    case ItemTypes.Boots:
                        items.Add(Instantiate(prefab, contentClothes));
                        items[items.Count - 1].GetComponent<StartPrefabItem>().setValues(item);
                        
                        break;
                    case ItemTypes.Chest:
                        items.Add(Instantiate(prefab, contentClothes));
                        items[items.Count - 1].GetComponent<StartPrefabItem>().setValues(item);
                        
                        break;
                    case ItemTypes.Shield:
                        items.Add(Instantiate(prefab, contentClothes));
                        items[items.Count - 1].GetComponent<StartPrefabItem>().setValues(item);
                        
                        break;
                    case ItemTypes.Casque:
                        items.Add(Instantiate(prefab, contentClothes));
                        items[items.Count - 1].GetComponent<StartPrefabItem>().setValues(item);
                        break;
                    default:
                        break;
                }

            }

        }
        GeneralEvents.activeItems(ItemTypes.Pistol);
    }

    public void shopActive(string type)
    {
        
        foreach (var item in items)
        {
            item.SetActive(true);
        }
        switch (type)
        {
            case "legs":
                GeneralEvents.activeItems(ItemTypes.legs);
                break;
            case "AK":
                GeneralEvents.activeItems(ItemTypes.AK);

                break;
            case "Pistol":
                GeneralEvents.activeItems(ItemTypes.Pistol);
                break;
            case "knife":
                GeneralEvents.activeItems(ItemTypes.knife);

                break;
            case "Boots":
                GeneralEvents.activeItems(ItemTypes.Boots);

                break;
            case "Chest":
                GeneralEvents.activeItems(ItemTypes.Chest);

                break;
            case "Shield":
                GeneralEvents.activeItems(ItemTypes.Shield);

                break;
            case "Casque":
                GeneralEvents.activeItems(ItemTypes.Casque);
                break;
            default:
                break;
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
