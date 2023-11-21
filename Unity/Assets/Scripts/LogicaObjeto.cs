using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaObjeto : MonoBehaviour
{
    private LogicaPlayer player;
    public int tipo;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<LogicaPlayer>();
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
        }
    }
}