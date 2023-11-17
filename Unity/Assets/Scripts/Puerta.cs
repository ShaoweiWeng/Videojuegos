using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public GameObject nokey;
    public GameObject key;
    public GameObject btnPuerta;
    public Animator animPuerta;
    public Animator animPuertab;

    void Start()
    {
        key.SetActive(false);
        nokey.SetActive(false);
        btnPuerta.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision){

        if(collision.tag.Equals("key")){

            Llave.llave +=1;
            Destroy(collision.gameObject); //elimina la llave
        }
    }
    private void OnTriggerStay2D(Collider2D collision){

        if(collision.tag.Equals("door")&& Llave.llave== 0)
        {
            nokey.SetActive(true);
        }
         if(collision.tag.Equals("door") && Llave.llave== 1)
        {
            key.SetActive(true);
            btnPuerta.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
         if(collision.tag.Equals("door")&& Llave.llave== 0)
        {
            nokey.SetActive(false);
        }
         if(collision.tag.Equals("door") && Llave.llave== 1)
        {
            key.SetActive(false);
            btnPuerta.SetActive(false);
        }

    }
    public void botonabrirP(){

        animPuerta.SetTrigger("abrir"); 
         animPuertab.SetTrigger("abrir"); 

    }
   
}
