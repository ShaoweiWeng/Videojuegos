using System.Collections;
using UnityEngine;

public class Instruccion : MonoBehaviour
{
public GameObject shift;
private bool onObject = false;
void Update() {
    if (onObject && Input.GetButtonDown("Interactuar"))
    {
        StartCoroutine(pusalShift());
    }

 }
private void OnTriggerStay2D(Collider2D other)
{
    onObject = true;
}
private IEnumerator pusalShift()
{
    shift.SetActive(true);
    yield return new WaitForSeconds(5f);
    shift.SetActive(false);
}
}
