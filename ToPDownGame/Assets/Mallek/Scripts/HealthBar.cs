using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public PlayerBehavior player;

    private void OnEnable()
    {
        player.damege += setBar;

    }

    private void OnDisable()
    {
        player.damege -= setBar;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
    private void setBar(int damege)
    {
        slider.value -= damege;
    }
}
