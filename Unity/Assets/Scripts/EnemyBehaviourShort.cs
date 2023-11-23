using System;
using System.Collections;
using UnityEngine;

public class EnemyBehaviourShort : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float attackingRange;
    public float attackRate = 3.5f;
    public Boolean alreadyAttacked = false;
    private Transform player;
    private float distanceFromPlayer;
    private Animator meleeAnimator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        meleeAnimator = GetComponentInChildren<EnemySword>().gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > attackingRange)
        {
            if(player.transform.position.x > transform.position.x){ //Si el enemigo esta a la izq del jugador
                transform.localScale = new Vector2( Mathf.Abs(transform.localScale.x), transform.localScale.y ); //el enemigo mira hacia la der
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else{
                transform.localScale = new Vector2( Mathf.Abs(transform.localScale.x)* -1, transform.localScale.y ); //el enemigo mira hacia la izq
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
        }
        else if (distanceFromPlayer <= attackingRange && !alreadyAttacked)
        {
            meleeAnimator.SetTrigger("ForwardMeleeSwipe");
            alreadyAttacked = true;
            StartCoroutine(attackReset());
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