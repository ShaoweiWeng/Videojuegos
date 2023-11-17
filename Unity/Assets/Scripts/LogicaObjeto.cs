using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaObjeto : MonoBehaviour
{
    public LogicaPlayer player;
    public int tipo;
    void start()
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
        }
    }
}