using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaLlavesBoss : MonoBehaviour
{
    private bool onKey = false;
    private Collider2D other;

    void Update()
    {
        if (onKey && Input.GetButtonDown("Interactuar"))
        {
            other.GetComponent<LogicaObjeto>().Efecto();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        this.other = other;
        if (other.CompareTag("key"))
        {
            onKey = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("key"))
        {
            onKey = false;
        }
    }

}
