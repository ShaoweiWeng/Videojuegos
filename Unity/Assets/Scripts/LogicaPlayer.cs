using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class LogicaPlayer : MonoBehaviour
{

    //-.-VARIABLES DE MOVIMIENTO HORIZONTAL-.-
    private Rigidbody2D rb;
    private float walkSpeed = 10;
    private float xAxis, yAxis;
    public bool isFacingLeft;
    private Vector2 facingLeft;

    //-.-VARIABLES DE MOVIMIENTO VERTICAL-.-
    private float jumpForce = 40;
    private float dashVelocity = 30f;
    private float dashingTime = 0.2f;
    public Transform groundChecker;
    private float groundCheckY = 0.2f;
    private float groundCheckX = 0.5f;
    public LayerMask whatIsGround;
    private bool canDash = true, isDashing;
    private Vector2 dashDirection;
    private TrailRenderer trailRenderer;
    private float maxVelocity = 35f;

    //-.-VARIABLES A GUARDAR-.-
    public bool dashObtenido = false;
    public bool llaveObtenido = false;
    public int llavesBoss = 0;

    private bool onObject = false;
    private Collider2D other;


    public bool enKnockb = false;   //true si el personaje está en knockback porque ha tocado un enemigo

    private void Awake()
    {
        loadData();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        isFacingLeft = false; // Suponiendo que siempre se va a spawnear mirando hacia la derecha

        facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (!enKnockb)
        {
            GetInputs();
            Walk();
            Jump();

            // GIRAR PERSONAJE
            if (xAxis > 0 && isFacingLeft)
            {
                isFacingLeft = false;
                Flip();
            }
            if (xAxis < 0 && !isFacingLeft)
            {
                isFacingLeft = true;
                Flip();
            }

            if (dashObtenido && Input.GetButtonDown("Dash") && canDash)
            {
                isDashing = true;
                canDash = false;
                trailRenderer.emitting = true;
                dashDirection = new Vector2(xAxis, yAxis);
                if (dashDirection == Vector2.zero)
                {
                    dashDirection = new Vector2(transform.localScale.x, 0);
                }
                StartCoroutine(routine: StopDashing());
            }
            if (isDashing)
            {
                rb.velocity = dashDirection.normalized * dashVelocity;
                return; // Sin este return puede hacer dos dash seguidos, si el primer dash lo hace desde una plataforma sin haber saltado
            }
            if (isGrounded()) canDash = true;
        }
        if (onObject && Input.GetButtonDown("Interactuar"))
        {
            other.GetComponent<LogicaObjeto>().Efecto();
            if (other.CompareTag("ObjetoEliminable")){
                Destroy(other.gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity); // Tope de velocidad
    }

    private void OnDestroy()
    {
        saveData();
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
    }

    private void Walk()
    {
        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
    }

    public bool isGrounded()
    {
        //PRIMER RAYCAST
        //origen raycast: groundChecker - Direccion: abajo
        // Cuanto viaja: groundCheckY  - Que detecta: suelo(whatIsGround)

        //SEGUNDO Y TERCER RAYCAST
        //para poder saltar cerca de bordes
        if (Physics2D.Raycast(groundChecker.position, Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundChecker.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundChecker.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        //VARIABLE HEIGHT JUMP
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        isDashing = false;
    }

    private void Flip()
    {
        if (isFacingLeft)
        {
            transform.localScale = facingLeft;
        }
        if (!isFacingLeft)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        this.other = other;
        onObject = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        onObject = false;
    }

    private void saveData()
    {
        PlayerPrefs.SetInt("dash", dashObtenido ? 1 : 0);
        PlayerPrefs.SetInt("llave", llaveObtenido ? 1 : 0);
        PlayerPrefs.SetInt("llavesBoss", llavesBoss);
    }
    private void loadData()
    {
        dashObtenido = PlayerPrefs.GetInt("dash", 0) == 1;
        llaveObtenido = PlayerPrefs.GetInt("llave", 0) == 1;
        llavesBoss = PlayerPrefs.GetInt("llavesBoss", 0);
    }
}