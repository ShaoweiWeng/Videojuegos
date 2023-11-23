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




    public void jugar(){

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
    }
    public void Salir()
    {
        Debug.Log("Salir...");
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
}
