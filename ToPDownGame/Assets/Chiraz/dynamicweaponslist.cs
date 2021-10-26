using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dynamicweaponslist : MonoBehaviour
{
    // Start is called before the first frame update
    public ListItems listitemsPrefab;
    public Transform contentWeapon, contentClothes;
    public GameObject prefabWeap, prefabClow;
    public List<GameObject> items;
    public Image ISelect,btPis,btAK, btKnife,btShield,btTop,btBot,btShoos,btCasque;
    public Text weaponSelect,damege,speed;
    public CurrentItem currentItem;
    public GameObject btnBuyClow, btnUseClow,btnBuyWeapon, btnUseWeapon,btAdsWeap,btAdsClow;

    public Text coinMenu, coinWeap, coinClow, coinShop, coinChar;
    private void setCoin(int coin)
    {
        coinChar.text = coin + "";
        coinClow.text = coin + "";
        coinMenu.text = coin + "";
        coinShop.text = coin + "";
        coinWeap.text = coin + "";
    }

    private void OnEnable()
    {
        GeneralEvents.showItem += show;
        GeneralEvents.toBuyClow += toBuyClow;
        GeneralEvents.toUseClow += toUseClow;
        GeneralEvents.isCurrentClow += isCurrentClow;

        GeneralEvents.toBuyWeapon += toBuyWeapon;
        GeneralEvents.toUseWeapon += toUseWeapon;
        GeneralEvents.isCurrentWeapon += isCurrentWeapon;

        GeneralEvents.btnUseIte += btnUseItem;

        GeneralEvents.setCoin += setCoin;
    }

    private void OnDisable()
    {
        GeneralEvents.showItem -= show;
        GeneralEvents.toBuyClow -= toBuyClow;
        GeneralEvents.toUseClow -= toUseClow;
        GeneralEvents.isCurrentClow -= isCurrentClow;

        GeneralEvents.toBuyWeapon -= toBuyWeapon;
        GeneralEvents.toUseWeapon -= toUseWeapon;
        GeneralEvents.isCurrentWeapon -= isCurrentWeapon;

        GeneralEvents.btnUseIte -= btnUseItem;

        GeneralEvents.setCoin += setCoin;
    }
    void Start() {
        
        foreach (ItemObjects item in listitemsPrefab.items)
        {
            if (item.visible)
            {
                switch (item.type)
                {
                    case ItemTypes.legs:
                        items.Add(Instantiate(prefabClow, contentClothes));
                        items[items.Count - 1].GetComponent<StartPrefabItem>().setValues(item);
                        break;
                    case ItemTypes.AK:
                        items.Add(Instantiate(prefabWeap, contentWeapon));
                        items[items.Count - 1].GetComponent<StartPrefabItem>().setValues(item);
                        
                        break;
                    case ItemTypes.Pistol:
                        items.Add(Instantiate(prefabWeap, contentWeapon));
                        items[items.Count - 1].GetComponent<StartPrefabItem>().setValues(item);
                        break;
                    case ItemTypes.knife:
                        
                        items.Add(Instantiate(prefabWeap, contentWeapon));
                        items[items.Count - 1].GetComponent<StartPrefabItem>().setValues(item);
                        if (currentItem.knife==item)
                        {
                            items[items.Count - 1].GetComponent<StartPrefabItem>().clicked();
                        }
                        break;
                    case ItemTypes.Boots:
                        items.Add(Instantiate(prefabClow, contentClothes));
                        items[items.Count - 1].GetComponent<StartPrefabItem>().setValues(item);
                        
                        break;
                    case ItemTypes.Chest:
                        items.Add(Instantiate(prefabClow, contentClothes));
                        items[items.Count - 1].GetComponent<StartPrefabItem>().setValues(item);
                        
                        break;
                    case ItemTypes.Shield:
                        items.Add(Instantiate(prefabClow, contentClothes));
                        items[items.Count - 1].GetComponent<StartPrefabItem>().setValues(item);
                        
                        break;
                    case ItemTypes.Casque:
                        items.Add(Instantiate(prefabClow, contentClothes));
                        items[items.Count - 1].GetComponent<StartPrefabItem>().setValues(item);
                        break;
                    default:
                        break;
                }

            }

        }
        GeneralEvents.activeItems(ItemTypes.knife);
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
                btCasque.color =new Color(0.4627451f, 0.4627451f, 0.4627451f);
                
                btTop.color = new Color(0.4627451f, 0.4627451f, 0.4627451f);
                btBot.color = Color.white;
                btShoos.color = new Color(0.4627451f, 0.4627451f, 0.4627451f);
                break;
            case "AK":
                GeneralEvents.activeItems(ItemTypes.AK);
                btAK.color = Color.white;
                btKnife.color =new Color(0.6320754f, 0.6320754f, 0.6320754f) ;
                btPis.color = new Color(0.6320754f, 0.6320754f, 0.6320754f);
                break;
            case "Pistol":
                GeneralEvents.activeItems(ItemTypes.Pistol);
                btAK.color = new Color(0.6320754f, 0.6320754f, 0.6320754f);
                btKnife.color = new Color(0.6320754f, 0.6320754f, 0.6320754f);
                btPis.color = Color.white;
                break;
            case "knife":
                GeneralEvents.activeItems(ItemTypes.knife);
                btAK.color = new Color(0.6320754f, 0.6320754f, 0.6320754f);
                btKnife.color = Color.white;
                btPis.color = new Color(0.6320754f, 0.6320754f, 0.6320754f);
                break;
            case "Boots":
                GeneralEvents.activeItems(ItemTypes.Boots);
                btCasque.color = new Color(0.4627451f, 0.4627451f, 0.4627451f);
                
                btTop.color = new Color(0.4627451f, 0.4627451f, 0.4627451f);
                btBot.color = new Color(0.4627451f, 0.4627451f, 0.4627451f);
                btShoos.color = Color.white;
                break;
            case "Chest":
                GeneralEvents.activeItems(ItemTypes.Chest);
                btCasque.color = new Color(0.4627451f, 0.4627451f, 0.4627451f);
                
                btTop.color =  Color.white;
                btBot.color = new Color(0.4627451f, 0.4627451f, 0.4627451f);
                btShoos.color = new Color(0.4627451f, 0.4627451f, 0.4627451f);
                break;
            
            case "Casque":
                GeneralEvents.activeItems(ItemTypes.Casque);
                btCasque.color = Color.white;
                
                btTop.color = new Color(0.4627451f, 0.4627451f, 0.4627451f);
                btBot.color = new Color(0.4627451f, 0.4627451f, 0.4627451f);
                btShoos.color = new Color(0.4627451f, 0.4627451f, 0.4627451f);
                break;
            default:
                break;
        }
        
    }
    void show(ItemObjects item)
    {
        
        if (item.GetType().Equals(typeof(WeaponItem)))
        {
            ISelect.sprite = item.spriteChoice;
            weaponSelect.text = item.nameItem;
        }
        else
        {
            GeneralEvents.setItem(item);
        }
        
        
    }

    public void buyWeapon()
    {
        GeneralEvents.buyWeapon();
    }
    public void buyClowths()
    {
        GeneralEvents.buyClowths();
    }

    public void use()
    {
        GeneralEvents.useIte();
    }
    void toUseWeapon()
    {
        btnUseWeapon.SetActive(true);
        btnBuyWeapon.SetActive(false);
        btAdsWeap.SetActive(true);
        btnUseWeapon.GetComponent<Button>().interactable = true;
        btAdsWeap.GetComponent<Button>().interactable = false;
    }
    void toBuyWeapon()
    {
        btAdsWeap.SetActive(true);
        btnUseWeapon.SetActive(false);
        btnBuyWeapon.SetActive(true);
        btnUseWeapon.GetComponent<Button>().interactable = true;
        btAdsWeap.GetComponent<Button>().interactable = true;
    }
    void isCurrentWeapon()
    {
        btAdsWeap.SetActive(true);
        btnUseWeapon.SetActive(true);
        btnBuyWeapon.SetActive(false);
        btnUseWeapon.GetComponent<Button>().interactable = false;
        btAdsWeap.GetComponent<Button>().interactable = false;
    }
    void toUseClow()
    {
        btAdsClow.SetActive(true);
        btnUseClow.SetActive(true);
        btnBuyClow.SetActive(false);
        btnUseClow.GetComponent<Button>().interactable = true;
        btAdsClow.GetComponent<Button>().interactable = false;
    }
    void toBuyClow()
    {
        btAdsClow.SetActive(true);
        btnUseClow.SetActive(false);
        btnBuyClow.SetActive(true);
        btnUseClow.GetComponent<Button>().interactable = true;
        btAdsClow.GetComponent<Button>().interactable = true;
    }
    void isCurrentClow()
    {
        btAdsClow.SetActive(true);
        btnUseClow.SetActive(true);
        btnBuyClow.SetActive(false);
        btnUseClow.GetComponent<Button>().interactable = false;
        btAdsClow.GetComponent<Button>().interactable = false;
    }

    void btnUseItem(ItemObjects item)
    {
        switch (item.type)
        {
            case ItemTypes.legs:
                currentItem.bot.state = StateItem.toUse;
                item.state = StateItem.current;
                currentItem.bot = item;
                break;
            case ItemTypes.AK:
                currentItem.ak.state = StateItem.toUse;
                item.state = StateItem.current;
                currentItem.ak = (WeaponItem) item;
                break;
            case ItemTypes.Pistol:
                currentItem.pistol.state = StateItem.toUse;
                item.state = StateItem.current;
                currentItem.pistol = (WeaponItem)item;
                break;
            case ItemTypes.knife:
                currentItem.knife.state = StateItem.toUse;
                item.state = StateItem.current;
                currentItem.knife = (WeaponItem)item;
                break;
            case ItemTypes.Boots:
                currentItem.shoos.state = StateItem.toUse;
                item.state = StateItem.current;
                currentItem.shoos = item;
                break;
            case ItemTypes.Chest:
                currentItem.top.state = StateItem.toUse;
                item.state = StateItem.current;
                currentItem.top = item;
                break;
            case ItemTypes.Casque:
                currentItem.casque.state = StateItem.toUse;
                item.state = StateItem.current;
                currentItem.casque = item;
                break;
            default:
                break;
        }
    }
}
