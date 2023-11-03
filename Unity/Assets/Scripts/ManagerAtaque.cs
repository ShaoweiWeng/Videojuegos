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
    private Player player;

    private void Start()
    {
        //anim = GetComponent<Animator>();
        player = GetComponent<Player>();
        meleeAnimator = GetComponentInChildren<Cuchillo>().gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        pulsadoAtaque = Input.GetButtonDown("Attack");

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
