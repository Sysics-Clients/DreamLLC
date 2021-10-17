

[System.Serializable]
public class GeneralPlayerData 
{
    public int coins;
    public int[] shop;
    public GeneralPlayerData(Singleton singleton)
    {
        
        coins = singleton.coins;
        shop = new int[singleton.items.items.Count];
        for (int i = 0; i < singleton.items.items.Count; i++)
        {
            shop[i] =(int) singleton.items.items[i].state;
        }
    }
}
