using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class InputSystem : MonoBehaviour
{   
    
    public Image miniMapDirectionImage;
    public GameObject ErreurText;
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
    public GameObject MissionObject;
    public RectTransform MissionRect;
    public Text MissionText;
    GameManager gameManager;
    Coroutine TextErreur;
    Tween ErreurTexttween;
    //MissionMessage missionMessage = null;
    private void OnEnable()
    {
        GeneralEvents.health += changeHealth;
        GeneralEvents.takeDamege += bloodEffect;
       // GeneralEvents.changeColorHealth += chageColorBar;
        GeneralEvents.changeColorWeaponButton += chageColorweaponButton;
        gameManager = GameObject.FindObjectOfType<GameManager>();
        GeneralEvents.onTaskFinish += SetMission;
        GeneralEvents.writeErrorMessage += afficherErreurMessage;
    }

    

    private void OnDisable()
    {
        GeneralEvents.health -= changeHealth;
        GeneralEvents.takeDamege -= bloodEffect;
       // GeneralEvents.changeColorHealth -= chageColorBar;
        GeneralEvents.changeColorWeaponButton -= chageColorweaponButton;
        GeneralEvents.onTaskFinish -= SetMission;
        GeneralEvents.writeErrorMessage -= afficherErreurMessage;

    }
    private void Start()
    {
        bulletAKStart.text = "/ "+GeneralEvents.nbBulletStart().x;
        bulletPistolStart.text = "/ " + GeneralEvents.nbBulletStart().y ;
        SelectMission();
    }
    void SelectMission()
    {
        Mission item;
        //foreach (Mission item in gameManager.currentLevel.missions)
        for (int i=0;i< gameManager.currentLevel.missions.Count;i++)
        {
            item = gameManager.currentLevel.missions[i];
            if (item.isCompleted==false)
            {
                MissionObject.SetActive(true);
                MissionRect.transform.localScale = new Vector3(0, 1, 1);
                MissionRect.DOScaleX(1, 0.5f).SetEase(Ease.Linear);
                MissionText.text = item.missionText;
                gameManager.currentMission = item;
                GeneralEvents.setMissionObjectAndSprite(item.MissionObject, item.MissionSprite);
                break;
            }
        }
    }
    void SetMission(MissionName missionName,int id=0)
    {
        if ((gameManager.currentMission.missionName == missionName)&&(id== gameManager.currentMission.missionId))
        {
            gameManager.currentMission.isCompleted = true;
            SelectMission();
        }
        else
        {
            for (int i= 0; i<gameManager.currentLevel.missions.Count;i++)
            {
                Mission item = gameManager.currentLevel.missions[i];
                if ((item.missionName == missionName) && (id == item.missionId))
                {
                    item.isCompleted = true;
                        for(int j = gameManager.currentLevel.missions.IndexOf(gameManager.currentMission); j < i; j++)
                        {
                            if (gameManager.currentLevel.missions[j].priority < item.priority)
                            {
                                gameManager.currentLevel.missions[j].isCompleted = true;
                            }
                        }
                    if (gameManager.currentMission.isCompleted)
                        SelectMission();
                }
            }
        }  
    }
    void afficherErreurMessage(string err)
    {
        if(TextErreur!=null)
            StopCoroutine(TextErreur);
        ErreurText.SetActive(true);
        ErreurText.GetComponent<Text>().text = err;
        
        ErreurText.GetComponent<Text>().color = Color.red;
        TextErreur =StartCoroutine(TranslateText());
        if(ErreurTexttween!=null)
            ErreurTexttween.Complete();
        ErreurText.transform.localPosition = Vector3.zero;
        ErreurTexttween = ErreurText.transform.DOMoveY(ErreurText.transform.position.y + 25, 2);

    }
    IEnumerator TranslateText()
    {
        while (ErreurText.GetComponent<Text>().color.a > 0)
        {
            yield return new WaitForSeconds(0.06f);
            ErreurText.GetComponent<Text>().color = new Color(ErreurText.GetComponent<Text>().color.r, ErreurText.GetComponent<Text>().color.g, ErreurText.GetComponent<Text>().color.b, ErreurText.GetComponent<Text>().color.a - 0.05f) ;
        }
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
       // move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
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
            yield return new WaitForSeconds(0.03f);
            sliderArmor.fillAmount = (int)(sliderArmor.fillAmount * 100) / 100.0f;
            c = StartCoroutine(smoothHealth(health, armor));
        }
        else if (armor > sliderArmor.fillAmount)
        {
            sliderArmor.fillAmount += 0.03f;
            yield return new WaitForSeconds(0.03f);
            c = StartCoroutine(smoothHealth(health, armor));
        }
        else if (health < sliderHelth.fillAmount)
        {
            sliderHelth.color = gradient.Evaluate(health);
            sliderHelth.fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.03f);
            sliderHelth.fillAmount = (int)(sliderHelth.fillAmount * 100) / 100.0f;
            c = StartCoroutine(smoothHealth(health, armor));
        }
        else if (health > sliderHelth.fillAmount)
        {
            sliderHelth.color = gradient.Evaluate(health);
            sliderHelth.fillAmount += 0.03f;
            yield return new WaitForSeconds(0.03f);
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