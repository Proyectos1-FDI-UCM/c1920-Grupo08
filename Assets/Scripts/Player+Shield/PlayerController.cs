using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed, jumpForce, climbSpeed;
    [SerializeField] LayerMask ground, ladder;

    CapsuleCollider2D capsule;

    float moveX, moveY, gravity;
    float ladderRange = 0.55f;

    Rigidbody2D rb;

    bool isCeilinged = false;
    bool isGrounded = false;
    bool isCrouching = false;
    bool jump = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        gravity = rb.gravityScale;
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
        }
        else if (isCrouching && !Input.GetButton("Crouch") && !isCeilinged)
        {
            isCrouching = false;
            capsule.size = new Vector2(capsule.size.x, 1.8f);
            capsule.offset = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * moveX, rb.velocity.y);

        if (jump && isGrounded)
        {
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, ladderRange);
    }
}
