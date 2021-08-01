﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputSystem : MonoBehaviour
{
    public Joystick MvtJoystic;
    public Joystick ShootJoystic;
    public Button HideButton;


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



}