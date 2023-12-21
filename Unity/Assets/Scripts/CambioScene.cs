using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CambioScene : MonoBehaviour
{
    [SerializeField] private Slider loadbar;
    [SerializeField] private GameObject loadPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            loadPanel.SetActive(true);
            StartCoroutine(LoadAsync());
        }
    }
    private IEnumerator LoadAsync()
    {
        int sigScene = SceneManager.GetActiveScene().buildIndex;
        // Carga la siguiente escena en el orden de construcción
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sigScene + 1);
        while (!asyncOperation.isDone)
        {
            loadbar.value = asyncOperation.progress / 0.9f;
            yield return null;

        }
    }
}


