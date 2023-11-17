using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongBullet : MonoBehaviour
{

    public float speed = 1f;
    Vector2 targetVector;
    GameObject playerTarget;
    Rigidbody2D bulletrb;
    Vector2 movementVector;


    // Start is called before the first frame update
    void Start()
    {
        bulletrb = GetComponent<Rigidbody2D>();
        playerTarget = GameObject.FindGameObjectWithTag("Player");

        targetVector = (playerTarget.transform.position - transform.position).normalized * speed;
        movementVector = new Vector2(targetVector.x, targetVector.y);
        bulletrb.velocity = movementVector;

        Destroy(this.gameObject,3);
    }
}
