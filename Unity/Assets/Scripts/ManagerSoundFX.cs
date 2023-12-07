using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSoundFX : MonoBehaviour
{

    [SerializeField] private AudioSource sfxObject;

    //Para poder ser llamado desde cualquier otro script directamente
    //sin tener que conseguir el componente
    //AVISO, SOLO PUEDE HABER UN UNICO SCRIPT MANAGERSOUNDFX EN CADA ESCENA
    public static ManagerSoundFX instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //Crea objeto que hace el sonido y lo destruye
    public void PlaySFX (AudioClip clip, Transform trans, float volume)
    {
        AudioSource audioSource = Instantiate(sfxObject, trans.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;

        audioSource.Play();

        float audioLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, audioLength);
    }

    //Igual que funcion anterior, pero elige de una lista de clips uno
    //al azar
    public void PlaySFXRandom (AudioClip[] clips, Transform trans, float volume)
    {
        int n = Random.Range(0, clips.Length);

        AudioSource audioSource = Instantiate(sfxObject, trans.position, Quaternion.identity);
        audioSource.clip = clips[n];
        audioSource.volume = volume;

        audioSource.Play();

        float audioLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, audioLength);
    }
}
