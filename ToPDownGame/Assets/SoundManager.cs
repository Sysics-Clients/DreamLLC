using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public AudioSource audioSource;
    private AudioClip GetClip()
    {
        return audioClips[Random.Range(0, audioClips.Count)];
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = GetClip();
            audioSource.Play();
        }
    }
}
