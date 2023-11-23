using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongBullet : MonoBehaviour
{
    GameObject playerTarget;
    float speed = 10f;
    Vector2 movementDir;

    void Start()
    {

        playerTarget = GameObject.FindGameObjectWithTag("Player");

        movementDir = (playerTarget.transform.position - transform.position).normalized;

        Destroy(this.gameObject,3);

    }

    void Update()
    {
        transform.Translate(movementDir * speed * Time.deltaTime);
        var posicion = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            playerTarget.GetComponent<HealthPlayer>().takeDamagePlayer(1);
            Destroy(this.gameObject);
        }
    }
}
