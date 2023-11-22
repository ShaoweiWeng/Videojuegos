using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourShort : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float attackingRange;
    public float attackRate = 5f;
    public Boolean alreadyAttacked = false;
    private Transform player;
    private float distanceFromPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > attackingRange)
        {
            if(player.transform.position.x > transform.position.x){ //Si el enemigo esta a la izq del jugador
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);    //el enemigo mira hacia la izq
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else{
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
        }
        else if (distanceFromPlayer <= attackingRange && !alreadyAttacked)
        {
            alreadyAttacked = true;
        }

    }

    private IEnumerator attackReset()
    {
        yield return new WaitForSeconds(attackRate);
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, attackingRange);
    }
}