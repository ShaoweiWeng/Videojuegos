using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaEspada : MonoBehaviour
{
    public GameObject botonF;
    private bool onEspada = false;
    private Collider2D other;

    void Update()
    {
        if (onEspada && Input.GetButtonDown("Interactuar"))
        {
            other.GetComponent<LogicaObjeto>().Efecto();
            Destroy(other.gameObject);
            botonF.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        this.other = other;
        if (other.CompareTag("Arma"))
        {
            botonF.SetActive(true);
            onEspada = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Arma"))
        {
            botonF.SetActive(false);
            onEspada = false;
        }
    }
}
