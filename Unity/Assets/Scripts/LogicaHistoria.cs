using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaHistoria : MonoBehaviour
{
    [SerializeField] private GameObject canva;
    [SerializeField] private GameObject informe2;

    private bool juegoPause = false;

    public void activarCanva()
    {
        if (!juegoPause)
        {
            juegoPause = true;
            Time.timeScale = 0f;
            canva.SetActive(true);
        }
        else
        {
            Resume();
        }
    }
    public void siguienteInforme()
    {
        canva.SetActive(false);
        informe2.SetActive(true);
    }
    public void Resume()
    {
        juegoPause = false;
        Time.timeScale = 1f;
        canva.SetActive(false);
        if (informe2 != null) informe2.SetActive(false);
    }
}
