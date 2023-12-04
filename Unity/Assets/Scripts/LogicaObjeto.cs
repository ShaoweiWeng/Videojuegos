using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogicaObjeto : MonoBehaviour
{
    private LogicaPlayer player;
    private ManagerAtaque espada;
    private MenuInicial menu;
    public int tipo;

    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    private int lineIndex;
    private bool dialogueStart;
    private Coroutine m_MyCoroutineReference;

    [SerializeField] private GameObject objeto;

    public GameObject botonF;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<LogicaPlayer>();
        espada = GameObject.FindGameObjectWithTag("Player").GetComponent<ManagerAtaque>();
        menu = objeto.GetComponent<MenuInicial>();
    }
    public void Efecto()
    {
        switch (tipo)
        {
            case 1:
                player.dashObtenido = true;
                break;
            case 2:
                player.llaveObtenido = true;
                break;
            case 3:
                espada.espadaObtenido = true;
                break;
            case 4:
                player.llavesBoss += 1;
                break;
            case 5:
                menu.Objeto();
                menu.juegoPause = true;
                break;
            case 6:
                if (!dialogueStart)
                {
                    StartDialogue();
                }
                else if (dialogueText.text == dialogueLines[lineIndex])
                {
                    nextLine();
                }
                else
                {
                    StopCoroutine(m_MyCoroutineReference);
                    dialogueText.text = dialogueLines[lineIndex];
                }
                break;

        }
    }

    private void StartDialogue()
    {
        Time.timeScale = 0f;
        dialogueStart = true;
        botonF.SetActive(false);
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
            botonF.SetActive(true);
            dialoguePanel.SetActive(false);
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

    private void OnTriggerStay2D(Collider2D other)
    {
        botonF.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        botonF.SetActive(false);
    }
}