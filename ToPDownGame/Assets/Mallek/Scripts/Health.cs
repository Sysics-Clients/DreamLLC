using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    
    public float corentHelth, armor;
    public PlayerBehavior player;
    private void OnEnable()
    {
        player.damege += damege;
    }
    private void OnDisable()
    {
        player.damege -= damege;
    }
    // Start is called before the first frame update
    void Start()
    {
        //sliderHelth.fillAmount = 1;
        //sliderArmor.fillAmount = 1;
        GeneralEvents.health(100,100);
        corentHelth = 100;
        armor = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(player!=null)
                player.damege(5);
        }
    }
    
    public void damege(float value)
    {
        if (armor > 0)
        {
            armor -= value;
            //sliderArmor.fillAmount = armor/100;
            
            if (armor < 0)
            {
                corentHelth += armor;
                //sliderHelth.fillAmount = corentHelth / 100;
                armor = 0;
                
            }
        }else if (corentHelth > 0)
        {
            corentHelth -= value;
            //sliderHelth.fillAmount = corentHelth / 100;

           
        }
        if (player != null && corentHelth <= 0)
        {
            print("aa");
            player.die();
            //player = null;
            player.damege -= damege;
            StartCoroutine(SleppGame());
            
        }
        //if (corentHelth < 30)
        //    GeneralEvents.changeColorHealth();
        GeneralEvents.takeDamege();   
        GeneralEvents.health(corentHelth, armor);

    }
    IEnumerator SleppGame()
    {
        yield return new WaitForSeconds(4);
        Time.timeScale = 0;
    }
    
}
