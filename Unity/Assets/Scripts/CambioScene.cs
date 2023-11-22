using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.tag == "Player"){
             int sigScene = SceneManager.GetActiveScene().buildIndex;

            // Carga la siguiente escena en el orden de construcción
            SceneManager.LoadScene(sigScene + 1);
        }
        }
           
    }
    

