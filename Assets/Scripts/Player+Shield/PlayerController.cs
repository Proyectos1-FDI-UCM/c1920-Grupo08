using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] float baseSpeed, jumpForce, climbSpeed;
    [SerializeField] LayerMask ground, ladder;

    CapsuleCollider2D capsule;

    float moveX, moveY, gravity;
    float ladderRange = 0.55f;
    float currentSpeed;

    Rigidbody2D rb;

    bool isCeilinged = false;
    bool isGrounded = false;
    bool isCrouching = false;
    bool jump = false;
    bool stunned = false;

    [SerializeField] Sound jumpSound;
    new AudioSource audio;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        gravity = rb.gravityScale;
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        CheckCollisions(true);
        CheckCollisions(false);

        jump = Input.GetButtonDown("Jump");
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        if (!isCrouching && Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
            capsule.size = new Vector2(capsule.size.x, 0.8f);
            capsule.offset = new Vector2(capsule.offset.x, -0.5f);
            animator.SetBool("isCrouching", true);
        }
        else if (isCrouching && !Input.GetButton("Crouch") && !isCeilinged)
        {
            isCrouching = false;
            capsule.size = new Vector2(capsule.size.x, 1.8f);
            capsule.offset = Vector2.zero;
            animator.SetBool("isCrouching", false);
        }

        if (Mathf.Abs(moveX) > 0)
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
    }

    void FixedUpdate()
    {
        if (!stunned)
        {
            rb.velocity = new Vector2(currentSpeed * moveX, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(moveX));
        }

        if (jump && isGrounded && !stunned)
        {
            AudioManager.instance.PlaySoundOnce(jumpSound);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jump = false;
        }

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
            currentSpeed = baseSpeed * (1f - 0.01f * weight);
        else if (weight > 50)
            currentSpeed = baseSpeed / 2;
        else
            currentSpeed = baseSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, ladderRange);
    }
}
