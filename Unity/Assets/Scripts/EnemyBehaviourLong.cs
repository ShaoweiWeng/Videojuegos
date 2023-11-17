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
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            StartCoroutine(shootingReset());
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