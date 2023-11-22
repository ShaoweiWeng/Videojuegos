using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaPuerta : MonoBehaviour
{
    public GameObject nokey;
    public GameObject key;
    public GameObject btnPuerta;
    public Animator animPuerta;
    public Animator animPuertab;
    public GameObject botonF;
    private LogicaPlayer player;

    private bool onKey = false;
    private bool onDoor = false;
    private Collider2D collision;

    public GameObject bloqueo;
    public GameObject bloqueo2;


    void Start()
    {
        key.SetActive(false);
        nokey.SetActive(false);
        btnPuerta.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<LogicaPlayer>();
    }

    void Update()
    {
        if (onKey && Input.GetButtonDown("Interactuar"))
        {
            collision.GetComponent<LogicaObjeto>().Efecto();
            Destroy(collision.gameObject);
            Destroy(bloqueo);
            Destroy(bloqueo2);
            botonF.SetActive(false);
        }
        if (onDoor && Input.GetButtonDown("Interactuar"))
        {
            animPuerta.SetTrigger("abrir");
            animPuertab.SetTrigger("abrir");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        this.collision = collision;
        if (collision.CompareTag("key"))
        {
            botonF.SetActive(true);
            onKey = true;
        }

        if (collision.CompareTag("door"))
        {
            onDoor = true;
            if (!player.llaveObtenido) { nokey.SetActive(true); }
            else
            {
                key.SetActive(true);
                btnPuerta.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("key"))
        {
            botonF.SetActive(false);
            onKey = false;
        }
        if (collision.CompareTag("door"))
        {
            if (!player.llaveObtenido) { nokey.SetActive(false); }
            else
            {
                key.SetActive(false);
                btnPuerta.SetActive(false);
            }
        }
    }

}
