using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
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
    public GameObject bloodImage;
    public Gradient gradient;
    Coroutine c;

    private void OnEnable()
    {
        GeneralEvents.health += changeHealth;
        GeneralEvents.takeDamege += bloodEffect;
       // GeneralEvents.changeColorHealth += chageColorBar;
        GeneralEvents.changeColorWeaponButton += chageColorweaponButton;
    }

    

    private void OnDisable()
    {
        GeneralEvents.health -= changeHealth;
        GeneralEvents.takeDamege += bloodEffect;
       // GeneralEvents.changeColorHealth -= chageColorBar;
        GeneralEvents.changeColorWeaponButton -= chageColorweaponButton;
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
                if (GeneralEvents.sendRoll!=null&& GeneralEvents.getCanChange())
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
                    if (GeneralEvents.changeWeopen(WeopenType.AK))
                    {
                        AkButtonWeopen.FireObj.SetActive(true);
                        AkButtonWeopen.IsSelected.gameObject.SetActive(true);
                        GunButtonWeopen.FireObj.SetActive(false);
                        GunButtonWeopen.IsSelected.gameObject.SetActive(false);
                    }
                    
                }
                break;
            case "gun":
                
                if (GeneralEvents.changeWeopen != null)
                {
                    if (GeneralEvents.changeWeopen(WeopenType.Gun))
                    {
                        AkButtonWeopen.FireObj.SetActive(false);
                        AkButtonWeopen.IsSelected.gameObject.SetActive(false);
                        GunButtonWeopen.FireObj.SetActive(true);
                        GunButtonWeopen.IsSelected.gameObject.SetActive(true);
                    }
                }
                break;
            default:
                break;
        }
    }
    public void changeHealth(float health, float armor)
    {
        /*sliderHelth.fillAmount = health / 100;
        sliderArmor.fillAmount = armor / 100;*/
        if(c!=null)
            StopCoroutine(c);
        c=StartCoroutine(smoothHealth(health / 100, armor / 100));
        
    }

    IEnumerator smoothHealth(float health, float armor)
    {
        if (armor< sliderArmor.fillAmount)
        {
            sliderArmor.fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            sliderArmor.fillAmount = (int)(sliderArmor.fillAmount * 100) / 100.0f;
            c = StartCoroutine(smoothHealth(health, armor));
        }
        else if (armor > sliderArmor.fillAmount)
        {
            sliderArmor.fillAmount += 0.03f;
            yield return new WaitForSeconds(0.01f);
            c = StartCoroutine(smoothHealth(health, armor));
        }
        else if (health < sliderHelth.fillAmount)
        {
            sliderHelth.color = gradient.Evaluate(health);
            sliderHelth.fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            sliderHelth.fillAmount = (int)(sliderHelth.fillAmount * 100) / 100.0f;
            c = StartCoroutine(smoothHealth(health, armor));
        }
        else if (health > sliderHelth.fillAmount)
        {
            sliderHelth.color = gradient.Evaluate(health);
            sliderHelth.fillAmount += 0.03f;
            yield return new WaitForSeconds(0.01f);
            c = StartCoroutine(smoothHealth(health, armor));
        }

    }
        public void bloodEffect()
    {
        StartCoroutine(blood());
    }
    IEnumerator blood() {
        bloodImage.active = true;
        Image bloodimg = bloodImage.GetComponent<Image>();
        bloodimg.color = new Color(bloodimg.color.r, bloodimg.color.g, bloodimg.color.b, 0);
        bloodimg.DOFade(1,0.1f).SetEase(Ease.Flash).SetLoops(-1);
        yield return new WaitForSeconds(0.5f);
        bloodImage.active = false;
    }
   /* private void chageColorBar()
    {

        sliderHelth.color = Color.red;
    }*/
    private void chageColorweaponButton(Color c,int w)
    {
        if (w==0)
        {
            AkButtonWeopen.IsSelected.color = c;
        }else if (w == 1)
        {
            GunButtonWeopen.IsSelected.color = c;
        }
        
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