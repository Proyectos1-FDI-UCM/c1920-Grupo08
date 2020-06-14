using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]

// Script de movimiento del jugador y activación del escudo.
public class PlayerController : MonoBehaviour
{
    [SerializeField] float baseSpeed, jumpForce, climbSpeed;
    [SerializeField] LayerMask ground, ladder;
    [SerializeField] GameObject shield;

    CapsuleCollider2D capsule;

    float moveX, moveY, gravity;
    float ladderRange = 0.55f;
    float shieldMoveSpeed;

    Rigidbody2D rb;

    bool isCeilinged = false;
    bool isGrounded = false;
    bool isCrouching = false;
    bool jump = false;
    bool stunned = false;
    bool shieldBroken = false;

    [SerializeField] Sound jumpSound;
    new AudioSource audio;
    Animator animator;
    float moveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        shield.SetActive(false);
        gravity = rb.gravityScale;
        moveSpeed = baseSpeed;
        AddWeight(22f);
    }

    void Update()
    {
        // Comprueba las colisiones con el suelo y el techo en cada momento posible.
        CheckCollisions(true);
        CheckCollisions(false);

        // Input
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }        

        // Control del agachado según tenga techo encima o no.
        if (!isCrouching && Input.GetButtonDown("Crouch") && isGrounded)
        {
            isCrouching = true;
            moveSpeed *= 0.5f;
            capsule.size = new Vector2(capsule.size.x, 0.8f);
            capsule.offset = new Vector2(capsule.offset.x, -0.5f);
            animator.SetBool("isCrouching", true);
        }

        else if (isCrouching && !Input.GetButton("Crouch") && !isCeilinged)
        {
            isCrouching = false;
            moveSpeed = baseSpeed;
            capsule.size = new Vector2(capsule.size.x, 1.8f);
            capsule.offset = Vector2.zero;
            animator.SetBool("isCrouching", false);
        }        

        // Control de sonido "continuado" al andar
        if (Mathf.Abs(moveX) > 0 && !stunned && Time.timeScale != 0)
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }

        else
        {
            audio.Stop();
        }

        // Control de la activación del escudo si este está roto o no.
        if (shieldBroken)
        {
            shield.SetActive(false);
        }

        else
        {
            if (Input.GetButtonDown("Fire1") && !shield.activeSelf)
            {

                shield.SetActive(true);
            }

            else if (Input.GetButtonUp("Fire1") && shield.activeSelf)
            {

                shield.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        // Físicas del movimiento horizontal
        if (!stunned)
        {
            if (!shieldBroken && Input.GetButton("Fire1") && shieldMoveSpeed < moveSpeed)
                rb.velocity = new Vector2(shieldMoveSpeed * moveX, rb.velocity.y);
            else
                rb.velocity = new Vector2(moveSpeed * moveX, rb.velocity.y);

            animator.SetFloat("Speed", Mathf.Abs(moveX));
        }

        else
        {
            animator.SetFloat("Speed", 0f);
        }

        // Físicas de salto
        if (jump && isGrounded && !stunned && !isCrouching)
        {
            jump = false;
            AudioManager.instance.PlaySoundOnce(jumpSound);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        // Físicas para trepar
        if (Physics2D.OverlapCircle(transform.position, ladderRange, ladder) != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, moveY * climbSpeed);
            rb.gravityScale = 0f;
        }

        else
        {
            rb.gravityScale = gravity;
        }
    }

    // Control de colisiones verticales y horizontales según la capsula de colision, su posición y tamaño
    // Puede controlar las colisiones inferiores y superiores según se le indique.
    // Usa un array de rayos para conprobar las colisiones de manera más precisa.
    void CheckCollisions(bool up)
    {
        Vector2[] Raycasts = new Vector2[3];
        RaycastHit2D[] rayHits = new RaycastHit2D[3];

        Vector2 rayPoint = new Vector2(transform.position.x, transform.position.y) + capsule.offset;
        float dis = 0.2f;

        Vector2 rayDirection;

        if (up) rayDirection = Vector2.up;
        else rayDirection = Vector2.down;

        Vector2 rayCenter = rayPoint + rayDirection * (capsule.size.y * 0.5f);

        Raycasts[0] = rayCenter + Vector2.left * capsule.size.x * 0.5f;
        Raycasts[1] = rayCenter;
        Raycasts[2] = rayCenter + Vector2.right * capsule.size.x * 0.5f;

        int count = 0;

        for (int i = 0; i < Raycasts.Length; i++)
        {
            rayHits[i] = Physics2D.Raycast(Raycasts[i], rayDirection, dis, ground);
            if (rayHits[i].point != Vector2.zero) count += 1;
        }

        if (count == 0)
        {
            if (up) isCeilinged = false;
            else isGrounded = false;
        }
        else
        {
            if (up) isCeilinged = true;
            else isGrounded = true;
        }
    }

    // Se encarga de paralizar al jugador
    public void Stunned(float time)
    {
        stunned = true;
        Invoke("StunCD", time);
        rb.velocity = Vector2.zero;
    }
    void StunCD()
    {
        stunned = false;
    }

    //El peso de los escudos escalará de 0 a 50, reduciendo la velocidad del jugador en un porcentaje igual al valor peso
    public void AddWeight(float weight)
    {
        if (weight <= 50 && weight >= 0)
            shieldMoveSpeed = baseSpeed * (1f - 0.01f * weight);
        else if (weight > 50)
            shieldMoveSpeed = baseSpeed / 2;
        else
            shieldMoveSpeed = baseSpeed;
    }

    // El GM puede avisar del estado del escudo para permitir o no su utilización
    public void ShieldBroken(bool state)
    {
        shieldBroken = state;
    }  
}
