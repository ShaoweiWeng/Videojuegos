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

    void Start()
    {
        key.SetActive(false);
        nokey.SetActive(false);
        btnPuerta.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<LogicaPlayer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("key"))
        {
            botonF.SetActive(true);
            if (Input.GetButtonDown("Interactuar"))
            {
                collision.GetComponent<LogicaObjeto>().Efecto();
                Destroy(collision.gameObject);
                botonF.SetActive(false);
            }
        }

        if (collision.tag.Equals("door") && !player.llaveObtenido)
        {
            nokey.SetActive(true);
        }
        if (collision.tag.Equals("door") && player.llaveObtenido)
        {
            key.SetActive(true);
            btnPuerta.SetActive(true);
            if (Input.GetButtonDown("Interactuar"))
            {
                animPuerta.SetTrigger("abrir");
                animPuertab.SetTrigger("abrir");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("key")) { botonF.SetActive(false); }
        if (collision.tag.Equals("door") && !player.llaveObtenido)
        {
            nokey.SetActive(false);
        }
        if (collision.tag.Equals("door") && player.llaveObtenido)
        {
            key.SetActive(false);
            btnPuerta.SetActive(false);
        }

    }

}
