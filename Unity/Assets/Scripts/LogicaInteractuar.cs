using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaInteractuar : MonoBehaviour
{
    public GameObject botonF;

    private void OnTriggerStay(Collider other)
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
    private void OnTriggerExit(Collider other)
    {
        botonF.SetActive(false);
    }
}
