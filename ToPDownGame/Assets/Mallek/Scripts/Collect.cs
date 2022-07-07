using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public int value;
    public Type type;
    public GameObject effect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            if (effect!=null)
            {
                GameObject obj = Instantiate(effect, other.transform);
                obj.transform.SetParent(other.transform);
                Destroy(obj, 3);
            }
            
            switch (type)
            {
                case Type.bulletAK:
                    other.GetComponent<Attack>().weapons[0].nbTotalBullet += value;
                    break;
                case Type.bulletPistol:
                    other.GetComponent<Attack>().weapons[1].nbTotalBullet += value;
                    break;
                case Type.health:
                    other.GetComponent<Health>().corentHelth += value;
                    if (other.GetComponent<Health>().corentHelth>100)
                    {
                        other.GetComponent<Health>().corentHelth = 100;
                    }
                    GeneralEvents.health(other.GetComponent<Health>().corentHelth, other.GetComponent<Health>().armor);
                    break;
                case Type.armor:
                    other.GetComponent<Health>().armor += value;
                    if (other.GetComponent<Health>().armor > 100)
                    {
                        other.GetComponent<Health>().armor = 100;
                    }
                    GeneralEvents.health(other.GetComponent<Health>().corentHelth, other.GetComponent<Health>().armor);
                    break;
                default:
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
public enum Type
{
    bulletAK,
    bulletPistol,
    health,
    armor
}
