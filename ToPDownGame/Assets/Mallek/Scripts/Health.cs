using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    const int maxHealth=100;
    public int corentHelth;
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
        corentHelth = maxHealth;
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
    
    public void damege(int value)
    {
        if (corentHelth > 0)
        {
            corentHelth -= value;
            if (corentHelth <= 0)
            {
                if (player != null)
                    player.state(MovmentControler.State.die);
            }
        }
    }
}
