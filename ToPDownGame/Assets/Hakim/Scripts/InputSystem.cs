using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputSystem : MonoBehaviour
{
    public Image OpenDoorIcon;
    public Joystick MvtJoystic;
    public Joystick ShootJoystic;
    public Button HideButton;
    public Image sliderHelth, sliderArmor;
    public Buttonweopen AkButtonWeopen;
    public Buttonweopen GunButtonWeopen;
    public Color NormalColor;
    public Color DisableColor;
    public Text bulletAK, bulletPistol;
    public Text bulletAKStart, bulletPistolStart;
    private void OnEnable()
    {
        GeneralEvents.health += changeHealth;
    }
    private void OnDisable()
    {
        GeneralEvents.health -= changeHealth;
    }
    private void Start()
    {
        bulletAKStart.text = "/ "+GeneralEvents.nbBulletStart().x;
        bulletPistolStart.text = "/ " + GeneralEvents.nbBulletStart().y ;
    }
    private void Update()
    {
        Vector3 move = Vector3.zero;
#if UNITY_EDITOR
         move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
#endif
#if UNITY_ANDROID
        move = new Vector3(MvtJoystic.Horizontal, 0, MvtJoystic.Vertical);
#endif
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 shootDir = new Vector3(ShootJoystic.Horizontal, 0, ShootJoystic.Vertical);
        
        
            if (GeneralEvents.sendShooting != null)
            {
                GeneralEvents.sendShooting(shootDir);
                bulletAK.text = GeneralEvents.nbBullet().x+" ";
                bulletPistol.text = GeneralEvents.nbBullet().y +" ";
                bulletAKStart.text = "/ " + GeneralEvents.nbBulletStart().x;
                bulletPistolStart.text = "/ " + GeneralEvents.nbBulletStart().y;
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
    public void SetWeopen(string weopen)
    {
        switch (weopen)
        {
            case "Ak":
                
                if (GeneralEvents.changeWeopen!=null)
                {
                    GeneralEvents.changeWeopen(WeopenType.AK);
                }
                AkButtonWeopen.FireObj.SetActive(true);
                AkButtonWeopen.IsSelected.gameObject.SetActive(true);
                GunButtonWeopen.FireObj.SetActive(false);
                GunButtonWeopen.IsSelected.gameObject.SetActive(false);
                break;
            case "gun":
                
                if (GeneralEvents.changeWeopen != null)
                {
                    GeneralEvents.changeWeopen(WeopenType.Gun);
                }
                AkButtonWeopen.FireObj.SetActive(false);
                AkButtonWeopen.IsSelected.gameObject.SetActive(false);
                GunButtonWeopen.FireObj.SetActive(true);
                GunButtonWeopen.IsSelected.gameObject.SetActive(true);
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
[System.Serializable]
public class Buttonweopen
{
    public GameObject FireObj;
    public Image IsSelected;
    public Text NbFire;
    public Text TotalFire;
}