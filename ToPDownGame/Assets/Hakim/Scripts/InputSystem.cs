using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class InputSystem : MonoBehaviour
{
    public GameObject WavesTextObject;
    public GameObject Flamethrower;
    public Text SDText;
    public GameObject AccessPanel;
    public GameObject CodePaperPanel;
    public Text Codetxt;
    private float ErreurImgYPos=76;
    public Image miniMapDirectionImage;
    public GameObject ErreurImg;
    public Image OpenDoorIcon;
    public Joystick MvtJoystic;
    public Joystick ShootJoystic;
    public Button HideButton;
    public Image sliderHelth, sliderArmor;
    public Buttonweopen AkButtonWeopen;
    public Buttonweopen GunButtonWeopen;
    public Buttonweopen BladeButtonWeopen;
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
    bool enableMovment=false;
    //MissionMessage missionMessage = null;
    private void OnEnable()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        GeneralEvents.health += changeHealth;
        GeneralEvents.takeDamege += bloodEffect;
       // GeneralEvents.changeColorHealth += chageColorBar;
        GeneralEvents.changeColorWeaponButton += chageColorweaponButton;
        gameManager = GameObject.FindObjectOfType<GameManager>();
        GeneralEvents.onTaskFinish += SetMission;
        GeneralEvents.writeErrorMessage += ErreurMessage;
        GeneralEvents.hideErreurMessage += HideMessageImg;
        GeneralEvents.shakeErreurMessage += ShakeMessage;
        GeneralEvents.newAccessCode += NewAccessCode;
        GeneralEvents.enableSD += ActivateAccessCode;
        GeneralEvents.waveMessage += WaveMessage;
    }

    

    private void OnDisable()
    {
        GeneralEvents.health -= changeHealth;
        GeneralEvents.takeDamege -= bloodEffect;
       // GeneralEvents.changeColorHealth -= chageColorBar;
        GeneralEvents.changeColorWeaponButton -= chageColorweaponButton;
        GeneralEvents.onTaskFinish -= SetMission;
        GeneralEvents.writeErrorMessage -= ErreurMessage;
        GeneralEvents.hideErreurMessage -= HideMessageImg;
        GeneralEvents.shakeErreurMessage -= ShakeMessage;
        GeneralEvents.newAccessCode -= NewAccessCode;
        GeneralEvents.enableSD -= ActivateAccessCode;
        GeneralEvents.waveMessage -= WaveMessage;

    }
    public void ActivateAccessCode()
    {
        AccessPanel.SetActive(true);
    }
    public void OnNumberClick(int i)
    {
        switch (i)
        {
            case -1:
                if(SDText.text.Equals(GameManager.instance.AccessCode))
                {
                    GeneralEvents.onTaskFinish(MissionName.enterAccessCode, 0);
                    AccessPanel.SetActive(false);
                    Flamethrower.SetActive(false);
                    GeneralEvents.writeErrorMessage("Flame Thrower Desactivated", Color.green);
                    GeneralEvents.hideErreurMessage(4);
                    GameObject.FindGameObjectWithTag("sd").gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                else
                {
                    SDText.text = "";
                    GeneralEvents.writeErrorMessage("Access Denied!", Color.red);
                    GeneralEvents.hideErreurMessage(4);
                }
                break;
            case -2:
                SDText.text = "";
                break;
            default:
                if (SDText.text.Length >= 4)
                {
                    GeneralEvents.writeErrorMessage("Only 4 Numbers required!", Color.red);
                    GeneralEvents.hideErreurMessage(4);
                    return;
                }
                print(i);
                SDText.text += i;
                break;
        }
    }
    public void NewAccessCode()
    {
        CodePaperPanel.SetActive(true);
        Codetxt.text=UnityEngine.Random.Range(1000, 10000).ToString();
        GameManager.instance.AccessCode = Codetxt.text;
        Time.timeScale=0;


    }
    public void exitCodePaperPanel()
    {
        Time.timeScale = 1;
        CodePaperPanel.SetActive(false);
    }
    IEnumerator setUpMovment()
    {
        yield return new WaitForSeconds(.2f);
        enableMovment = true;
    }
        private void Start()
    {
        StartCoroutine(setUpMovment());
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
       MissionObjects mo;
        foreach(GameObject obj in GameManager.instance.MiniMapTasks)
        {
            mo = obj.GetComponent<MissionObjects>();
            if (missionName == mo.missionName && id == mo.id)
            {
                obj.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level3":
                if (GeneralEvents.testAllCompletion())
                {
                    GeneralEvents.toNewScene("Level4");
                }
                break;
            case "Level4":
                if (GeneralEvents.testAllCompletion())
                {
                    GeneralEvents.toNewScene("Level5");
                }
                break;
            case "Level5":
                if (GeneralEvents.testAllCompletion())
                {
                    GeneralEvents.toNewScene("Level6");
                }
                break;
            case "Level6":
                if (GeneralEvents.testAllCompletion())
                {
                    GeneralEvents.toNewScene("Level7");
                }
                break;
            case "Level8":
                if (!GeneralEvents.testAllCompletion(MissionName.CompleteWaves))
                {
                    gameManager.currentLevel.missions[1].isCompleted = true;
                    zombiesManager.instance.NewWave();
                }
                else
                {
                    GeneralEvents.toNewScene("Level9");
                }
                break;
            case "Level9":
                if (GeneralEvents.testAllCompletion())
                {
                    GeneralEvents.toNewScene("Level10");
                }
                break;
        }
    }
   /* void afficherErreurMessage(string err)
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

    }*/
   public void WaveMessage(string msg)
    {
        Color color = WavesTextObject.GetComponent<Text>().color;
        color.a = 1;
        WavesTextObject.GetComponent<Text>().color = color;
        WavesTextObject.SetActive(true);
        WavesTextObject.GetComponent<Text>().text = msg;
        StartCoroutine(setInvisible());
    }
    IEnumerator setInvisible()
    {
        yield return new WaitForSeconds(2);
        while (WavesTextObject.GetComponent<Text>().color.a > 0)
        {
            yield return new WaitForSeconds(0.1f);
            Color color = WavesTextObject.GetComponent<Text>().color;
            color.a -= 0.05f;
            WavesTextObject.GetComponent<Text>().color = color;
        }
        WavesTextObject.SetActive(false);
    }
   public void ErreurMessage(string err,Color color)
    {
        
            ErreurImg.SetActive(true);
            ErreurImg.GetComponent<Image>().color = color;
            ErreurImg.GetComponentInChildren<Text>().text = err;
           if (ErreurTexttween != null)
                ErreurTexttween.Complete();
            ErreurTexttween = ErreurImg.GetComponent<RectTransform>().transform.DOLocalMoveY(250, 2);
        
    } 
    IEnumerator HideMessage(float time)
    {
        yield return new WaitForSeconds(time);
        ErreurTexttween = ErreurImg.transform.DOLocalMoveY(350, 2);
    }
    public void HideMessageImg(float time=0)
    {
        StartCoroutine(HideMessage(time));
    }
    public void ShakeMessage()
    {
        ErreurTexttween = ErreurImg.transform.DOShakeRotation(1);
    }
    private void Update()
    {
        if (enableMovment == false)
        {
            return;
        }
        Vector3 move = Vector3.zero;
#if UNITY_EDITOR
         move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
#endif
#if UNITY_ANDROID
        move = new Vector3(MvtJoystic.Horizontal, 0, MvtJoystic.Vertical);
#endif
         //move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 shootDir = new Vector3(ShootJoystic.Horizontal, 0, ShootJoystic.Vertical);
        
        
            if (GeneralEvents.sendShooting != null)
            {
                GeneralEvents.sendShooting(shootDir);
                if (GeneralEvents.nbBullet!=null)
                {
                bulletAK.text = GeneralEvents.nbBullet().x + " ";
                bulletPistol.text = GeneralEvents.nbBullet().y + " ";
                bulletAKStart.text = "/ " + GeneralEvents.nbBulletStart().x;
                bulletPistolStart.text = "/ " + GeneralEvents.nbBulletStart().y;
            }
                
            }
        
        
            if (GeneralEvents.sendMvt!=null)
            {
                if(shootDir != Vector3.zero && GeneralEvents.getWeaponType() == ItemTypes.knife)
                {
                    GeneralEvents.sendMvt(Vector3.zero);
                }
                else
                {
                GeneralEvents.sendMvt(move);
                }
                    
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
                    if (GeneralEvents.changeWeopen(ItemTypes.AK))
                    {
                        AkButtonWeopen.FireObj.SetActive(true);
                        AkButtonWeopen.IsSelected.gameObject.SetActive(true);
                        GunButtonWeopen.FireObj.SetActive(false);
                        GunButtonWeopen.IsSelected.gameObject.SetActive(false);
                        //BladeButtonWeopen.FireObj.SetActive(false);
                        BladeButtonWeopen.IsSelected.gameObject.SetActive(false);
                    }
                    
                }
                break;
            case "gun":
                
                if (GeneralEvents.changeWeopen != null)
                {
                    if (GeneralEvents.changeWeopen(ItemTypes.Pistol))
                    {
                        AkButtonWeopen.FireObj.SetActive(false);
                        AkButtonWeopen.IsSelected.gameObject.SetActive(false);
                        GunButtonWeopen.FireObj.SetActive(true);
                        GunButtonWeopen.IsSelected.gameObject.SetActive(true);
                        //BladeButtonWeopen.FireObj.SetActive(false);
                        BladeButtonWeopen.IsSelected.gameObject.SetActive(false);
                    }
                }
                break;
            case "blade":

                if (GeneralEvents.changeWeopen != null)
                {
                    if (GeneralEvents.changeWeopen(ItemTypes.knife))
                    {
                        AkButtonWeopen.FireObj.SetActive(false);
                        AkButtonWeopen.IsSelected.gameObject.SetActive(false);
                        GunButtonWeopen.FireObj.SetActive(false);
                        GunButtonWeopen.IsSelected.gameObject.SetActive(false);
                        //BladeButtonWeopen.FireObj.SetActive(true);
                        BladeButtonWeopen.IsSelected.gameObject.SetActive(true);
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