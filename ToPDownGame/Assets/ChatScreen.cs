using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ChatScreen : MonoBehaviour
{
    public List<GameObject> messages;
    public RectTransform MaskImage;
    public string SgtSmithText;
    public Text SgtText;
    public GameObject SgtMessage;
    public GameObject ContinueButton;
    public AudioSource audioSource;
    public AudioClip popup;
    public PadController padController;
    private void OnEnable()
    {
        Time.timeScale = 0;
        foreach (var item in messages)
        {
            item.SetActive(false);
        }
        StartCoroutine(StartShowingMsg());
    }
    IEnumerator StartShowingMsg()
    {
        for (int i = 0; i < messages.Count; i++)
        {
            messages[i].SetActive(true);
            audioSource.clip = popup;
            audioSource.Play();
            yield return new WaitForSecondsRealtime(2f);
            if (i<messages.Count-1)
            {
                for (int k = 0; k < 10; k++)
                {
                    yield return new WaitForSecondsRealtime(0.02f);
                    MaskImage.Translate(Vector3.up*30);
                }
                
                
            }
            else
            {
                SgtMessage.SetActive(true);
                for (int j = 0; j < SgtSmithText.Length; j++)
                {
                    yield return new WaitForSecondsRealtime(0.02f);
                    SgtText.text += SgtSmithText[j];
                    if (j==SgtSmithText.Length-11)
                    {
                        ContinueButton.SetActive(true);
                    }
                }
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
        
        

    }
    public void DisableCanvas()
    {
        padController.canvasInput.SetActive(true);
        padController.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
