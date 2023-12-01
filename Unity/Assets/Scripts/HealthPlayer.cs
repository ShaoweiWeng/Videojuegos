using System.Collections;
using UnityEngine;
using System;


public class HealthPlayer : MonoBehaviour
{
    //-.-.-ATRIBUTOS - VIDA-.-.-
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth;
    [SerializeField] private float invincibilityTime = 1.5f;
    [SerializeField] private float healTime = 30f;
    public bool noDamageModeActivated = false;

    private Coroutine m_MyCoroutineReference;
    private bool invencible;

    //-.-.-ATRIBUTOS - SPRITES-.-.-
    [SerializeField] private UnityEngine.UI.Image[] heartsInHealthBar;
    [SerializeField] private Sprite fullHeart, emptyHeart;

    //-.-.-ATRIBUTOS - KNOCKBACK-.-.-
    private Rigidbody2D rb;
    private float force = 100;
    [SerializeField] private float knockbackTime = .4f;
    private Vector2 direction;


    //-.-.-EVENTOS - GAMEOVER -.-.-
    public event EventHandler MuerteJugador;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        createHealthBar();
        invencible = false;
        rb = GetComponent<Rigidbody2D>();
        m_MyCoroutineReference = StartCoroutine(heal());
    }


    //Estas funciones se llaman en el objeto que hace daño al jugador
    public void takeDamagePlayer(int damage)
    {
        if (!invencible && currentHealth > 0 && !noDamageModeActivated)
        {
            StopCoroutine(m_MyCoroutineReference);
            m_MyCoroutineReference = StartCoroutine(heal());
            invencible = true;
            currentHealth = currentHealth - damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                MuerteJugador?.Invoke(this, EventArgs.Empty);//Para crear un evento

                //Destroy(gameObject);
                //AÑADIR ANIMACIONES DE MUERTE + GAMEOVER ETC
            }
            else
            {
                StartCoroutine(invincibility());
            }
        }
        updateHealthBar();
    }

    public void healDamagePlayer(int heal)
    {
        currentHealth = currentHealth + heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        updateHealthBar();
    }

    //Añade una barra de vida con tantos corazones como maxHealth
    private void createHealthBar()
    {
        for (int i = 0; i < heartsInHealthBar.Length; i++)
        {
            if (i < maxHealth)
            {
                heartsInHealthBar[i].enabled = true;
            }
            else
            {
                heartsInHealthBar[i].enabled = false;
            }
        }
    }

    // Actualiza la barra de vida, cambiando los corazones por corazones vacios o llenos dependiendo de la vida actual
    private void updateHealthBar()
    {
        for (int i = 0; i < heartsInHealthBar.Length; i++)
        {
            if (i < currentHealth)
            {
                heartsInHealthBar[i].sprite = fullHeart;
            }
            else
            {
                heartsInHealthBar[i].sprite = emptyHeart;
            }
        }
    }

    //EL JUGADOR HA TOCADO A UN ENEMIGO
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !invencible)
        {

            GetComponent<LogicaPlayer>().enKnockb = true;

            if (GetComponent<LogicaPlayer>().isFacingLeft)
            {  //APLICAMOS KNOCKBACK DEPENDIENDO DE LA DIRECCIÓN
                direction = new Vector2(4, 2);
                rb.AddForce(direction * force);
            }
            else
            {
                direction = new Vector2(-4, 2);
                rb.AddForce(direction * force);
            }

            takeDamagePlayer(1);
            StartCoroutine(knockback());
        }
    }


    private IEnumerator invincibility()
    {
        // AÑADIR ANIMACION PARPADEO
        yield return new WaitForSeconds(invincibilityTime);    // Esperar tiempo de invlunerabilidad
        invencible = false; // Puede ser golpeado otra vez
    }

    private IEnumerator knockback()
    {
        yield return new WaitForSeconds(knockbackTime);
        GetComponent<LogicaPlayer>().enKnockb = false;
    }

    private IEnumerator heal()
    {
        while (true)
        {
            yield return new WaitForSeconds(healTime);
            healDamagePlayer(1);
        }
    }
}
