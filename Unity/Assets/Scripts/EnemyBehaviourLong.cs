using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourLong : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float shootingRange;
    public GameObject bullet;
    public GameObject bulletParent;
    public float fireRate = 2f;
    public Boolean alreadyShot = false;
    private Transform player;
    private float distanceFromPlayer;
    [SerializeField] private AudioClip clipShoot;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange && !alreadyShot)
        {
            alreadyShot = true;
            ManagerSoundFX.instance.PlaySFX(clipShoot, transform, 1f);
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            StartCoroutine(shootingReset());
        }

        if((player.transform.position.x + 2 > transform.position.x) && (player.transform.position.x - 2 < transform.position.x)){ //Si el enemigo esta arriba del jugador
                transform.localScale = new Vector2( Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y ); //el enemigo mira hacia la der
            }
        else if(player.transform.position.x > transform.position.x){ //Si el enemigo esta a la izq del jugador
                transform.localScale = new Vector2( Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y ); //el enemigo mira hacia la der
            }
        else{
            transform.localScale = new Vector2( Mathf.Abs(transform.localScale.x), transform.localScale.y ); //el enemigo mira hacia la izq
        }

    }

    private IEnumerator shootingReset()
    {
        yield return new WaitForSeconds(fireRate);
        alreadyShot = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}