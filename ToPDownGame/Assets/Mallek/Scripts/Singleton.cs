using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton _instance;
    #region data
    public int coins=1000;
    public ListItems items;
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
            for (int i = 0; i < items.items.Count; i++)
            {
                items.items[i].state = (StateItem)data.shop[i];
            }
        }
        
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        load();
        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            //Rest of your Awake code

        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
