using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaInteractuar : MonoBehaviour
{
    public GameObject botonF;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Dash"))
        {
            botonF.SetActive(true);
            if (Input.GetButtonDown("Interactuar"))
            {
                other.GetComponent<LogicaObjeto>().Efecto();
                Destroy(other.gameObject);
                botonF.SetActive(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        botonF.SetActive(false);
    }
}
