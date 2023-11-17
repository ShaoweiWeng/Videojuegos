using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuar : MonoBehaviour
{
    public GameObject habilidad;
    public GameObject botonF;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            botonF.SetActive(true);
            if (Input.GetButtonDown("Interactuar"))
            {
                other.gameObject.GetComponent<Player>().dashObtenido = true;
                habilidad.SetActive(false);
                botonF.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        botonF.SetActive(false);
    }
}
