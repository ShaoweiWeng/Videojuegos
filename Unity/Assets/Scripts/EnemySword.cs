using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
 private int puntosDaño = 1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<HealthPlayer>())
        {
            collider.GetComponent<HealthPlayer>().takeDamagePlayer(puntosDaño);
        }
    }
}
