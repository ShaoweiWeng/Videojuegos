using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;


public class MenuInicial : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

    [SerializeField] private GameObject menuinicial;

    [SerializeField] private GameObject menuOpciones;
    [SerializeField] private GameObject menuAccesibilidad;

    [SerializeField] private Slider loadbar;
    [SerializeField] private GameObject loadPanel;

    private HealthPlayer healtplayer;

    private bool juegoPause = false;

    private LogicaPlayer player; //ARREGLO 4 LLAVES


    private void Start()
    {

        healtplayer = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPlayer>();
        healtplayer.MuerteJugador += ActivarMenu;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<LogicaPlayer>(); //ARREGLO 4 LLAVES

    }
    private void ActivarMenu(object sender, EventArgs e)
    {
        menuinicial.SetActive(true);
    }

    public void jugar()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadAsync());
    }

    private IEnumerator LoadAsync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        while (!asyncOperation.isDone)
        {
            loadbar.value = asyncOperation.progress / 0.9f;
            yield return null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPause)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        juegoPause = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);

    }
    public void Resume()
    {
        juegoPause = false;
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        menuOpciones.SetActive(false);
        menuAccesibilidad.SetActive(false);
    }
    public void Salir()
    {
        Application.Quit();
    }
    public void Reiniciar()
    {
        int actual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(actual);
        player.llavesBoss = 0; //ARREGLO 4 LLAVES

    }
    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }

    public void abrirOpciones()
    {
        menuPausa.SetActive(false);
        menuOpciones.SetActive(true);
    }

    public void abrirAccesibilidad()
    {
        menuOpciones.SetActive(false);
        menuAccesibilidad.SetActive(true);
    }

    public void vidaInfinita()
    {
        healtplayer.noDamageModeActivated = !healtplayer.noDamageModeActivated;
    }

}
