using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class ManagerAtaque : MonoBehaviour
{
    public float fuerzaDefault = 100;   // Con cuanta fuerza se mueve debido al knockback
    public float fuerzaArriba = 65;    // Con cuanta fuerza se mueve si se ataca hacia abajo
    public float tiempoKnockback = .1f; // Cuanto tiempo se va a mover debido al knockback
    private bool pulsadoAtaque;
    private Animator meleeAnimator;
    //private Animator anim;
    private LogicaPlayer player;

    //-.-VARIABLES A GUARDAR-.-
    public bool espadaObtenido = false;

    //-.-AUDIO-.-
    [SerializeField] private AudioClip clipEspadazo;

    private void Awake()
    {
        loadData();
    }

    private void Start()
    {
        //anim = GetComponent<Animator>();
        player = GetComponent<LogicaPlayer>();
        meleeAnimator = GetComponentInChildren<Cuchillo>().gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        CheckInput();
    }

    private void OnDestroy()
    {
        saveData();
    }

    private void CheckInput()
    {
        if (espadaObtenido)
        {
            pulsadoAtaque = Input.GetButtonDown("Attack");

             if (pulsadoAtaque){
                ManagerSoundFX.instance.PlaySFX(clipEspadazo, transform, 1f);
             }

            // ATAQUE ARRIBA
            if (pulsadoAtaque && Input.GetAxis("Vertical") > 0)
            {
                //anim.SetTrigger("UpwardsMelee");
                meleeAnimator.SetTrigger("UpwardsMeleeSwipe");
            }
            // ATAQUE ABAJO
            if (pulsadoAtaque && Input.GetAxis("Vertical") < 0 && !(player.isGrounded()))
            {
                //anim.SetTrigger("DownwardsMelee");
                meleeAnimator.SetTrigger("DownwardsMeleeSwipe");
            }
            // ATAQUE IZQ O DER (ATAQUE DEFAULT)
            if ((pulsadoAtaque && Input.GetAxis("Vertical") == 0) || pulsadoAtaque && (Input.GetAxis("Vertical") < 0 && player.isGrounded()))
            {
                //anim.SetTrigger("ForwardMelee");
                meleeAnimator.SetTrigger("ForwardMeleeSwipe");
            }
        }
    }

    private void saveData()
    {
        PlayerPrefs.SetInt("espada", espadaObtenido ? 1 : 0);
    }
    private void loadData()
    {
        espadaObtenido = PlayerPrefs.GetInt("espada", 0) == 1;
    }
}
