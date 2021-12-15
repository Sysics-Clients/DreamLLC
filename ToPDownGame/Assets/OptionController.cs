using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    public AudioMixer soundMixer;



   public Slider musicsSlider;
    public Slider EffectsSlider;
    public GameObject thisPanel;

    private void OnEnable()
    {
        
        if (!PlayerPrefs.HasKey("music"))
        {
            musicsSlider.value = 0.5f;
            soundMixer.SetFloat("MasterVolume", Mathf.Log10(musicsSlider.value) * 20);
            PlayerPrefs.SetFloat("music", musicsSlider.value);
        }
        else
        {
            float datasound = PlayerPrefs.GetFloat("music");
            musicsSlider.value = datasound;
            if (datasound<=0)
            {
                datasound = -80;
            }
            soundMixer.SetFloat("MasterVolume", Mathf.Log10(datasound) * 20);

        }

        if (!PlayerPrefs.HasKey("effect"))
        {
            EffectsSlider.value = 0.5f;
            soundMixer.SetFloat("MasterVolumeEffect", Mathf.Log10(EffectsSlider.value) * 20);
            PlayerPrefs.SetFloat("effect", EffectsSlider.value);
        }
        else
        {
            float datasound = PlayerPrefs.GetFloat("effect");
            EffectsSlider.value = datasound;
            if (datasound<=0)
            {
                datasound = -80;
            }
            soundMixer.SetFloat("MasterVolumeEffect", Mathf.Log10(datasound ) * 20);

        }

    }

    public void changeSoundMixer(Slider s)

    {
        if (s.value == 0)
        {
            soundMixer.SetFloat("MasterVolume", -80);
        }
        else
        {
            soundMixer.SetFloat("MasterVolume", Mathf.Log10(s.value) * 20);
        }

    }

   
    public void changeEffectMixer(Slider s)

    {
        if (s.value == 0)
        {
            soundMixer.SetFloat("MasterVolumeEffect", -80);
        }
        else
        {
            soundMixer.SetFloat("MasterVolumeEffect", Mathf.Log10(s.value) * 20);
        }


    }

    public void ChangePanelStat(string n)
    {
        if (n=="Save")
        {
            PlayerPrefs.SetFloat("effect", EffectsSlider.value);
            PlayerPrefs.SetFloat("music", musicsSlider.value);

        }
        
        thisPanel.SetActive(false);
    }
}
