using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEvents : MonoBehaviour
{
    public delegate void AddCounter();
    public static AddCounter addCounter;

    public delegate void EnnemyDown(string nameEnemy);
    public static EnnemyDown ennemyDown;
}
