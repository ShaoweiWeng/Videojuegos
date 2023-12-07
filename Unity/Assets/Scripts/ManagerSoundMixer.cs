using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ManagerSoundMixer : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider barraMaster;
    [SerializeField] private Slider barraMusic;
    [SerializeField] private Slider barraEffect;

    private void Awake()
    {
        float master, music, effect;
        if (audioMixer.GetFloat("masterVolume", out master) && master != 0) barraMaster.value = Mathf.Pow(10, master / 20f);
        if (audioMixer.GetFloat("musicVolume", out music) && music != 0) barraMusic.value = Mathf.Pow(10, music / 20f);
        if (audioMixer.GetFloat("soundFXVolume", out effect) && effect != 0) barraEffect.value = Mathf.Pow(10, effect / 20f);
    }

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
