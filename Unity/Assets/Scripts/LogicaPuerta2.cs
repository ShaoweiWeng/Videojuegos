using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaPuerta2 : MonoBehaviour
{
    public GameObject nokey;
    public GameObject key;
    public GameObject btnPuerta;
    public Animator animPuerta;
    public Animator animPuertab;
    private LogicaPlayer player;

    private bool onKey = false;
    private bool onDoor = false;
    private bool puertaAbierta = false;
    private Collider2D collision;

    //-.-.-ATRIBUTOS - SONIDO-.-.-
    [SerializeField] private AudioClip clipPuerta;


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
            Destroy(collision.gameObject);
        }
        if (player.llavesBoss == 4 && onDoor && Input.GetButtonDown("Interactuar") && !puertaAbierta)
        {
            puertaAbierta = true;
            ManagerSoundFX.instance.PlaySFX(clipPuerta, transform, 1f);
            animPuerta.SetTrigger("abrir");
            animPuertab.SetTrigger("abrir");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        this.collision = collision;
        if (collision.CompareTag("key"))
        {
            onKey = true;
        }

        if (collision.CompareTag("door"))
        {
            onDoor = true;
            if (player.llavesBoss != 4) { nokey.SetActive(true); }
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
            onKey = false;
        }
        if (collision.CompareTag("door"))
        {
            onDoor = false;
            if (player.llavesBoss != 4) { nokey.SetActive(false); }
            else
            {
                key.SetActive(false);
                btnPuerta.SetActive(false);
            }
        }
    }

}