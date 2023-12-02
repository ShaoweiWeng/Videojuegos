using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class VidaEnemigo : MonoBehaviour
{
    //-.-.-ATRIBUTOS-.-.-
    [SerializeField] private bool puedeSerDañado = true; //Ej, las paredes pueden ser atacadas y dan knockback, pero no tienen vida
    [SerializeField] private int vidaTotal = 100;
    [SerializeField] private int vidaActual;
    private float tiempoInvulnerabilidad = .2f;
    public bool daKnockbackUp = true;   // True si al hacer un ataque hacia abajo, el jugador rebota de este objeto
    private bool yaGolpeado;    // True si este objeto acaba de ser atacado

    //-.-.-ATRIBUTOS - CAMBIO DE COLOR-.-.-
    SpriteRenderer mySprite;
    Color oldColor;
    Color colorGolpe;

    void Start()
    {
        vidaActual = vidaTotal;

        mySprite = this.GetComponent<SpriteRenderer>();
        oldColor = mySprite.color;
        colorGolpe = new Color(1, 0, 0, 1);
    }

    public void GenerarDaño(int cantidad)
    {
        if ( puedeSerDañado && !yaGolpeado && vidaActual>0 )
        {
            yaGolpeado = true;
            vidaActual = vidaActual - cantidad;
            if (vidaActual <= 0)
            {
                //AÑADIR ANIMACIONES DE MUERTE
                gameObject.SetActive(false);
            }
            else{
                StartCoroutine(terminaGolpe());
            }
        }
    }

    private IEnumerator terminaGolpe()
    {
        mySprite.color = colorGolpe; //Cuando es golpeado, cambia de color brevemente
        yield return new WaitForSeconds(tiempoInvulnerabilidad);    // Esperar tiempo de invlunerabilidad
        mySprite.color = oldColor; //recupera su color original
        yaGolpeado = false; // Puede ser golpeado otra vez
    }
}