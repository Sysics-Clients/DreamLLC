﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image sliderHelth,sliderArmor;
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
        sliderHelth.fillAmount = 1;
        sliderArmor.fillAmount = 1;
        corentHelth = 100;
        armor = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
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
            sliderArmor.fillAmount = armor/100;
            if (armor < 0)
            {
                corentHelth -= 100 - armor;
                sliderHelth.fillAmount = corentHelth / 100;
                armor = 0;
            }
        }else if (corentHelth > 0)
        {
            corentHelth -= value;
            sliderHelth.fillAmount = corentHelth / 100;

            if (player != null && corentHelth<=0)
                player.die();
        }
        
    }
}
