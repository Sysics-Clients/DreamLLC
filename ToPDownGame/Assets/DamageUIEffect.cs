using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUIEffect : MonoBehaviour
{
    public List<Damagecanvas> damagecanvas;
    

    public void ActivateDammage(float health,Vector3 pos)
    {
        foreach (var item in damagecanvas)
        {
            if (item.gameObject.activeInHierarchy==false)
            {
                item.transform.position = pos;
                item.GetDamage(health);
                item.gameObject.SetActive(true);
                StartCoroutine(DisableGameObject(item.gameObject));
                break;
            }
        }
    }

    private void OnEnable()
    {
        GeneralEvents.enemyDamage += ActivateDammage;
    }

    private void OnDisable()
    {
        GeneralEvents.enemyDamage -= ActivateDammage;
    }
    IEnumerator DisableGameObject(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        obj.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //ActivateDammage(ennemie.transform.position);
        }
    }
}
