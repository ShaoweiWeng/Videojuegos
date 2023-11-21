using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchesEnemy : MonoBehaviour
{

    Rigidbody2D rb;
    private float force = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Enemy")){
            
            var opposite = -rb.velocity;
            rb.AddForce(opposite * Time.deltaTime * force);
        }
    }
}
