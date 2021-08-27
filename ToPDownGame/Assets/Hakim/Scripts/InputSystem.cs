using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputSystem : MonoBehaviour
{
    public Joystick MvtJoystic;
    public Joystick ShootJoystic;
    public Button HideButton;
    public Image sliderHelth, sliderArmor;

    private void OnEnable()
    {
        GeneralEvents.health += changeHealth;
    }
    private void OnDisable()
    {
        GeneralEvents.health -= changeHealth;
    }
    private void Update()
    {
#if UNITY_EDITOR
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
#endif
#if UNITY_ANDROID
        Vector3 move = new Vector3(MvtJoystic.Horizontal, 0, MvtJoystic.Vertical);
#endif

        Vector3 shootDir = new Vector3(ShootJoystic.Horizontal, 0, ShootJoystic.Vertical);
        
        
            if (GeneralEvents.sendShooting != null)
            {
                GeneralEvents.sendShooting(shootDir);
            }
        
        
            if (GeneralEvents.sendMvt!=null)
            {
                GeneralEvents.sendMvt(move);
            }
        
    }
    public void SetState(string statut)
    {
        switch (statut)
        {
            case "roll":
                if (GeneralEvents.sendRoll!=null)
                {
                    GeneralEvents.sendRoll();
                }
                break;
            default:
                break;
        }
    }

    public void changeHealth(float health, float armor)
    {
        sliderHelth.fillAmount = health / 100;
        sliderArmor.fillAmount = armor / 100;
    }

}
