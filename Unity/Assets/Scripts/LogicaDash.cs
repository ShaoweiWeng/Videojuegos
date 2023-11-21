using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaDash : MonoBehaviour
{
    public GameObject botonF;
    private bool onDash = false;
    private Collider2D other;

    void Update()
    {
        if (onDash && Input.GetButtonDown("Interactuar"))
        {
            other.GetComponent<LogicaObjeto>().Efecto();
            Destroy(other.gameObject);
            botonF.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        this.other = other;
        if (other.CompareTag("Dash"))
        {
            botonF.SetActive(true);
            onDash = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Dash"))
        {
            botonF.SetActive(false);
            onDash = false;
        }
    }
}
