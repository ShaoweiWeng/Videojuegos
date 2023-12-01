using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaObjeto : MonoBehaviour
{
    private LogicaPlayer player;
    private ManagerAtaque espada;
    public int tipo;

    public GameObject botonF;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<LogicaPlayer>();
        espada = GameObject.FindGameObjectWithTag("Player").GetComponent<ManagerAtaque>();
    }
    public void Efecto()
    {
        switch (tipo)
        {
            case 1:
                player.dashObtenido = true;
                break;
            case 2:
                player.llaveObtenido = true;
                break;
            case 3:
                espada.espadaObtenido = true;
                break;
            case 4:
                player.llavesBoss += 1;
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        botonF.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {

        botonF.SetActive(false);

    }
}