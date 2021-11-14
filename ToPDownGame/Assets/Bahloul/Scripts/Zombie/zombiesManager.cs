using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombiesManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static zombiesManager instance;
    public List<GameObject> Zombies;
    public byte waveNumber;
    public byte currentWave = 0;
    private void Awake()
    {
        instance = this;
        

    }
    void Start()
    {
        GameManager.instance.currentLevel.missions.Clear();
        Zombies = new List<GameObject>();
        GameObject[] gameObj = GameObject.FindGameObjectsWithTag("zombie");
        foreach (GameObject obj in gameObj)
        {
            Zombies.Add(obj);

        }
        int i;
        for (i = 0; i < waveNumber; i++)
        {
            GameManager.instance.currentLevel.AddMission(MissionName.KillAllZombies, i);
            GameManager.instance.currentLevel.missions[i].missionText = "Kill the zombies of wave " + (i + 1);
        }
    }
    public bool testActiveZombies()
    {
        
        foreach (GameObject obj in Zombies)
        {
            if (obj.activeInHierarchy)
                return false;
        }
        return true;
    }
    public void NewWave()
    {
        foreach (GameObject obj in Zombies)
        {
            obj.GetComponent<ZombieBehavior>().CurrentHealth *= 1.2f;
            obj.GetComponent<ZombieBehavior>().CurrentDamage *= 1.2f;
        }
        GeneralEvents.waveMessage("WAVE "+(currentWave+2));
        StartCoroutine(WaitForNewWave());
        
    }
    IEnumerator WaitForNewWave()
    {
       
        yield return new WaitForSeconds(4);
        currentWave += 1;
        for (int i = 0; i < Zombies.Count; i++)
        {

            Zombies[i].SetActive(true);
            Zombies[i].GetComponent<ZombieBehavior>().enemyState(ZombieState.State.Idle);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
