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
    public Image ISelect, IPresent, btPis,btAK, btKnife,btTop,btBot,btShoos,btCasque;
    public Text weaponSelect;
    public CurrentItem currentItem;
    public GameObject btnBuyClow, btnUseClow, btnAdsClow, btnBuyWeapon, btnUseWeapon,btnAdsWepon;
    int coin;
    public Text coinMenu, coinWeap, coinClow, coinShop, coinChar;
    public Text nbAds,nbShowAds;
    public Image damegeBar, speedBar, rateBar, reloadBar;
    
    private void setCoin(int coin)
    {
        coinChar.text = coin + "";
        coinClow.text = coin + "";
        coinMenu.text = coin + "";
        coinShop.text = coin + "";
        coinWeap.text = coin + "";
    }

    IEnumerator changeCoin(int coin)
    {
        yield return new WaitForSeconds(0.5f);
       
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
        EventController.videoRewarded += AdsRewardState;
        EventController.chnageButtonRewardRequest += enableAdsButton;

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

        GeneralEvents.setCoin -= setCoin;
        EventController.videoRewarded -= AdsRewardState;
        EventController.chnageButtonRewardRequest -= enableAdsButton;

    }
    void enableAdsButton(bool check)
    {
        btnAdsWepon.GetComponent<Button>().interactable = check;
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
    private void Update()
    {
        coinMenu.text = Singleton._instance.coins + "C";
        coinWeap.text = Singleton._instance.coins + "C";
        coinClow.text = Singleton._instance.coins + "C";
        coinShop.text = Singleton._instance.coins + "C";
        coinChar.text = Singleton._instance.coins + "C";
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
            IPresent.sprite = ((WeaponItem)item).presentation;
            weaponSelect.text = item.nameItem;
            damegeBar.fillAmount = (float)(((WeaponItem)item).damege+(100.0f/((101.0f-((WeaponItem)item).damege))/10.0f)) / 70.0f;
            speedBar.fillAmount = ((WeaponItem)item).speed / 10.0f;
            rateBar.fillAmount = ((WeaponItem)item).wait / 1;
            reloadBar.fillAmount = ((WeaponItem)item).reload / 50.0f;
            nbAds.text = item.nbVideo + " Ads";
            
            btnAdsWepon.SetActive(true);
            switch (item.state)
            {
                case StateItem.toUse:
                    btnBuyWeapon.SetActive(false);
                    btnUseWeapon.SetActive(true);
                    btnUseWeapon.GetComponent<Button>().interactable = true;
                    break;
                case StateItem.current:
                    btnBuyWeapon.SetActive(false);
                    btnUseWeapon.SetActive(true);
                    btnUseWeapon.GetComponent<Button>().interactable = false;
                    break;
                case StateItem.toBuy:
                    btnBuyWeapon.SetActive(true);
                    break;

                
                default:
                    break;
            }
        }
        else
        {
            if(item!=null)
            GeneralEvents.setItem(item);
            nbShowAds.text = item.nbVideo + " Ads";
        }
        
        
    }

    public void buyWeapon()
    {
        GeneralEvents.buyWeapon();
    }
    public void UseAdsWeapon()
    {
       
        AdsManager._instance.ShowRewardVideo("DefaultRewardedVideo");
    }
    void AdsRewardState(bool check)
    {
        if (check==true)
        {
            GeneralEvents.useAds();
        }
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
        btnUseWeapon.GetComponent<Button>().interactable = true;
    }
    void toBuyWeapon()
    {
        btnBuyWeapon.SetActive(true);
        btnUseWeapon.SetActive(false);
    }
    void isCurrentWeapon()
    {
        btnBuyWeapon.SetActive(false);
        btnUseWeapon.SetActive(true);
        btnUseWeapon.GetComponent<Button>().interactable = false;
    }
    void toUseClow()
    {
        btnAdsClow.SetActive(false);
        btnBuyClow.SetActive(false);
        btnUseClow.SetActive(true);
        btnUseClow.GetComponent<Button>().interactable = true;
    }
    void toBuyClow()
    {
        btnAdsClow.SetActive(true);
        btnBuyClow.SetActive(true);
        btnUseClow.SetActive(false);
    }
    void isCurrentClow()
    {
        btnAdsClow.SetActive(false);
        btnBuyClow.SetActive(false);
        btnUseClow.SetActive(true);
        btnUseClow.GetComponent<Button>().interactable = false;
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
