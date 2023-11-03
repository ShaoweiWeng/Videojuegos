using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Scripting.APIUpdating;

public class Cuchillo : MonoBehaviour
{
    private int puntosDaño = 20;
    private Player player;
    private Rigidbody2D rb;
    private ManagerAtaque managerAtaque;
    private Vector2 direccion;  //direccion de knockback
    private bool hayKnockBack;  //Indica si el jugador esta afectado por el knockback en este momento o no
    private bool esAtaqueHaciaAbajo;

    void Start()
    {
        player = GetComponentInParent<Player>();
        rb = GetComponentInParent<Rigidbody2D>();
        managerAtaque = GetComponentInParent<ManagerAtaque>();
    }

    void Update()
    {
        MovimientoHandler();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<VidaEnemigo>()) // COMPRUEBA SI SE ESTÁ ATACANDO A UN OBJETO CON SCRIPT VidaEnemigo
        {
            KnockBackHandler(collider.GetComponent<VidaEnemigo>());
        }
    }

    private void KnockBackHandler(VidaEnemigo objHealth)
    {
        // ATAQUE HACIA ABAJO EN EL AIRE
        // Si el objeto puede dar knockBack hacia arriba y el jugador esta atacando hacia abajo en el aire
        if (objHealth.daKnockbackUp && Input.GetAxis("Vertical") < 0 && !(player.isGrounded()) )
        {
            direccion = Vector2.up;
            esAtaqueHaciaAbajo = true;
            hayKnockBack = true;
        }
        // ATAQUE HACIA LA ARRIBA EN EL AIRE (un ataque hacia arriba en el suelo no da knockback)
        if (Input.GetAxis("Vertical") > 0 && !(player.isGrounded()))
        {
            direccion = Vector2.down;
            hayKnockBack = true;
        }
        // ATAQUE EN EL SUELO
        if ((Input.GetAxis("Vertical") <= 0 && player.isGrounded()) || Input.GetAxis("Vertical") == 0)
        {
            // ATAQUE HACIA LA IZQUIERDA
            if (player.isFacingLeft) // si se esta mirando hacia la izq
            {
                direccion = Vector2.right;
            }
            // ATAQUE HACIA LA DERECHA
            else
            {
                direccion = Vector2.left;
            }
            hayKnockBack = true;
        }
        objHealth.GenerarDaño(puntosDaño); // Hace daño al objeto
        StartCoroutine(terminaKnockBack());
    }

    private void MovimientoHandler()
    {
        if (hayKnockBack)
        {
            if (esAtaqueHaciaAbajo)
            {
                rb.AddForce(direccion * managerAtaque.fuerzaArriba);
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, 10f); // Tope de velocidad
            }
            else
            {
                rb.AddForce(direccion * managerAtaque.fuerzaDefault);
            }
        }
    }

    private IEnumerator terminaKnockBack()
    {
        //Waits in the amount of time setup by the meleeAttackManager script; this is by default .1 seconds
        yield return new WaitForSeconds(managerAtaque.tiempoKnockback);    // Esperar tiempoKnockback
        esAtaqueHaciaAbajo = false;
        hayKnockBack = false;
    }
}