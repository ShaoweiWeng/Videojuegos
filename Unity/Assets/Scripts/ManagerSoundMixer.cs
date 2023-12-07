using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ManagerSoundMixer : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float n)
    {
        //Mathf.Log10(n) * 20f para hacer la funcion lineal
        audioMixer.SetFloat("masterVolume", Mathf.Log10(n) * 20f);
    }

    public void SetMusicVolume(float n)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(n) * 20f);
    }

    public void SetFXVolume(float n)
    {
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(n) * 20f);
    }
}
