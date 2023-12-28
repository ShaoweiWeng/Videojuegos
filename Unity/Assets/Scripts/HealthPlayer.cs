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
    private float force = 1000;
    [SerializeField] private float knockbackTime = .4f;
    private Vector2 direction;

    //-.-.-EVENTOS - GAMEOVER -.-.-
    public event EventHandler MuerteJugador;

    //-.-.-ATRIBUTOS - PARPADEO-.-.-
    SpriteRenderer mySprite;
    Color oldColor;
    Color colorParpadeo;
    private Coroutine corutineParpadeo;

    //-.-.-ATRIBUTOS - SONIDO-.-.-
    [SerializeField] private AudioClip[] clipsDaño;
    [SerializeField] private AudioClip clipMuerte;


    private LogicaPlayer player;

    private void Awake()
    {
        loadData();
    }
    void Update()
    {
        updateHealthBar();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<LogicaPlayer>();

        currentHealth = maxHealth;
        createHealthBar();
        invencible = false;
        rb = GetComponent<Rigidbody2D>();
        m_MyCoroutineReference = StartCoroutine(heal());

        mySprite = this.GetComponent<SpriteRenderer>();
        oldColor = mySprite.color;
        colorParpadeo = new Color(1, 0, 0, 1);
    }

    private void OnDestroy()
    {
        saveData();
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
                ManagerSoundFX.instance.PlaySFX(clipMuerte, transform, 1f);
            }
            else
            {
                ManagerSoundFX.instance.PlaySFXRandom(clipsDaño, transform, 1f);
                StartCoroutine(invincibility());
            }
        }
    }

    public void healDamagePlayer(int heal)
    {
        currentHealth = currentHealth + heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
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

            player.enKnockb = true;

            if (player.isFacingLeft)
            {  //APLICAMOS KNOCKBACK DEPENDIENDO DE LA DIRECCIÓN
                Quaternion rotation = Quaternion.Euler(0, 0, 335); //force diagonal derecha
                direction = Vector2.up;
                rb.AddForce(rotation * direction * force);
            }
            else
            {
                Quaternion rotation = Quaternion.Euler(0, 0, 25); //force diagonal izquierda
                direction = Vector2.up;
                rb.AddForce(rotation * direction * force);
            }

            takeDamagePlayer(1);
            StartCoroutine(knockback());
        }
    }


    private IEnumerator invincibility()
    {
        corutineParpadeo = StartCoroutine(parpadeo()); // ANIMACION PARPADEO
        yield return new WaitForSeconds(invincibilityTime);    // Esperar tiempo de invlunerabilidad
        StopCoroutine(corutineParpadeo); // Paramos parpadeo
        mySprite.color = oldColor; // Devolvemos el sprite a su color original
        invencible = false; // Puede ser golpeado otra vez
    }

    private IEnumerator knockback()
    {
        yield return new WaitForSeconds(knockbackTime);
        player.enKnockb = false;
    }

    private IEnumerator heal()
    {
        while (true)
        {
            yield return new WaitForSeconds(healTime);
            healDamagePlayer(1);
        }
    }

    private IEnumerator parpadeo()
    {
        while (true)
        {
            mySprite.color = colorParpadeo;
            yield return new WaitForSeconds(0.2f);
            mySprite.color = oldColor;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void saveData()
    {
        PlayerPrefs.SetInt("vidaInfinita", noDamageModeActivated ? 1 : 0);
        PlayerPrefs.SetInt("vida", currentHealth);
    }
    private void loadData()
    {
        noDamageModeActivated = PlayerPrefs.GetInt("vidaInfinita", 0) == 1;
        currentHealth = PlayerPrefs.GetInt("vida", maxHealth);
    }

}
