using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Damagecanvas : MonoBehaviour
{
    public Text DamageText;

    public void GetDamage(float health)
    {
        DamageText.text = "-" + health;
    }
}
