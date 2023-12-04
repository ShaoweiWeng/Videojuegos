using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

    [SerializeField] private GameObject menuinicial;

    [SerializeField] private GameObject menuOpciones;
    [SerializeField] private GameObject menuAccesibilidad;

    private HealthPlayer healtplayer;

    private bool juegoPause = false;


    private void Start()
    {

        healtplayer = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPlayer>();
        healtplayer.MuerteJugador += ActivarMenu;

    }
    private void ActivarMenu(object sender, EventArgs e)
    {
        menuinicial.SetActive(true);
    }

    public void jugar()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
