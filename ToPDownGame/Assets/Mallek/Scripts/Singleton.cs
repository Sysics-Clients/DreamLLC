using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class Singleton : MonoBehaviour
{
    public static Singleton _instance;
    #region data
    public int coins=1000;
    public int Level;
    public ListItems items;
    public CurrentItem current;
    public AudioMixer soundMixer;
    #endregion
    
    #region save&load
    public void save()
    {
        SaveLoad.save(this);
    }
    public void load()
    {
        GeneralPlayerData data = SaveLoad.load();
        if (data!=null)
        {
            coins = data.coins;
            Level=data.Level;
            for (int i = 0; i < data.shop.Length; i++)
            {
                if (data.shop[i]==1 )
                    switch (items.items[i].type)
                    {
                        case ItemTypes.legs:
                            current.bot = items.items[i];
                            break;
                        case ItemTypes.AK:
                            current.ak =(WeaponItem) items.items[i];
                            break;
                        case ItemTypes.Pistol:
                            current.pistol = (WeaponItem)items.items[i];
                            break;
                        case ItemTypes.knife:
                            current.knife = (WeaponItem)items.items[i];
                            break;
                        case ItemTypes.Boots:
                            current.shoos = items.items[i];

                            break;
                        case ItemTypes.Chest:
                            current.top = items.items[i];
                            break;
                        case ItemTypes.Casque:
                            current.casque = items.items[i];
                            break;
                        default:
                            break;
                    }
                items.items[i].state = (StateItem)data.shop[i];
                print(data.shop[i]);
            }
        }
        
        
    }
    #endregion
    // Start is called before the first frame update
    void Awake()
    {
        
         if (_instance != null) {
     Destroy(gameObject);
   }else{
     _instance = this;
     DontDestroyOnLoad(gameObject);
   }
        load();
    }
    private void Start()
    {
        if (GeneralEvents.setCoin!=null)
        {
            setCoin();
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCoin()
    {
        GeneralEvents.setCoin(coins);
    }

    public void changeSoundMixer(Slider s)
       
    {
        if (s.value==0)
        {
            soundMixer.SetFloat("MasterVolume", -80);
        }
        else
        {
            soundMixer.SetFloat("MasterVolume", Mathf.Log10(s.value) * 20);
        }
        
    }
}
