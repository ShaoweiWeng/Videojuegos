using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogicaMonologo : MonoBehaviour
{
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    private int lineIndex;
    private bool dialogueStart;
    private Coroutine m_MyCoroutineReference;
    private bool onDialogo = false;



    private void StartDialogue()
    {
        Time.timeScale = 0f;
        dialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        m_MyCoroutineReference = StartCoroutine(showLine());
    }

    private void nextLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            m_MyCoroutineReference = StartCoroutine(showLine());
        }
        else
        {
            Time.timeScale = 1f;
            dialogueStart = false;
            dialoguePanel.SetActive(false);
            Destroy(gameObject);
        }
    }

    private IEnumerator showLine()
    {
        dialogueText.text = string.Empty;
        foreach (char c in dialogueLines[lineIndex])
        {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onDialogo = true;
        }
        StartDialogue();
    }
    void Update()
    {
        if (onDialogo && Input.GetButtonDown("Interactuar"))
        {
            if (dialogueText.text == dialogueLines[lineIndex])
            {
                nextLine();
            }
            else
            {
                StopCoroutine(m_MyCoroutineReference);
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }
}
