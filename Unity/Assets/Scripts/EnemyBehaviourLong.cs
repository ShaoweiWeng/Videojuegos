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
        else if (distanceFromPlayer <= shootingRange)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}