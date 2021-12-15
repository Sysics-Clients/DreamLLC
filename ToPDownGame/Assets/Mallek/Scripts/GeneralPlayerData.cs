

[System.Serializable]
public class GeneralPlayerData 
{
    public int coins;
    public int Level;
    public int[] shop;
    public string[] names;
    public GeneralPlayerData(Singleton singleton)
    {
        
        coins = singleton.coins;
        Level = singleton.Level;
        shop = new int[singleton.items.items.Count];
        names = new string[singleton.items.items.Count];
        for (int i = 0; i < singleton.items.items.Count; i++)
        {
            shop[i] =(int) singleton.items.items[i].state;
            names[i] = singleton.items.items[i].name;
        }
    }
}
