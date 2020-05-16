/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    public bool crouch;
    [Range(0, 1)] public float crouchspeed;
    public PlayerMove playerMove;
    public LayerMask ground;

    public Transform ceilingCheck; // Punto superior del personaje
    private float ceilingRadius = 0.5f; // Radio para el detector de techo

    Animator animator;
    CapsuleCollider2D col; // Collider del personaje
    private void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    private void FixedUpdate()
    {
        playerCrouch(crouch);
    }

    public void playerCrouch(bool crouch)
    {
        // Si el jugador intenta levantarse...
        if (!crouch)
        {
            // ...pero un esta bloqueado por un techo se lo impedimos.
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, ground))
            {
                // Lo mantiene agachado y...
                crouch = true;
            }

            // ... y no esta bloqueado por un techo.
            else
            {
                playerMove.speed = 10f;
                // ...aumenta el tamaño del collider del personaje al original
                col.size = new Vector2(col.size.x, 1.6f);
                // ...avisa al animador.
                animator.SetBool("isCrouching", false);
            }
        }

        // Si el jugador se agacha...
        else
        {
            // ...reduce la velocidad,
            playerMove.speed = crouchspeed * 10;

            // ...reduce el tamaño del collider del personaje,
            col.size = new Vector2(col.size.x, 0.8f);

            // ...avisa al animador.
            animator.SetBool("isCrouching", true);
        }
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerCrouch : MonoBehaviour
{
    bool crouch;
    public LayerMask ground;

    public Transform ceilingCheck; // Punto superior del personaje
    private float ceilingRadius = 0.5f; // Radio para el detector de techo

    Animator animator;
    CapsuleCollider2D col; // Collider del personaje
    private void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        
        if (!crouch && Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (crouch && !Input.GetButton("Crouch") && !IsBlocked())
        {
            crouch = false;
        }
    }

    private void FixedUpdate()
    {
        playerCrouch();
    }

    public bool IsCrouched()
    {
        return crouch;
    }
    private bool IsBlocked()
    {
        return Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, ground);
    }

    public void playerCrouch()
    {/*
        // Si el jugador intenta levantarse...
        if (crouch)
        {
            // ...pero un esta bloqueado por un techo se lo impedimos.
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, ground))
            {
                // Lo mantiene agachado y...
                crouch = true;
            }

            // ... y no esta bloqueado por un techo.
            else
            {
                crouch = false;
                // ...aumenta el tamaño del collider del personaje al original
                col.size = new Vector2(col.size.x, 1.6f);
                // ...avisa al animador.
                animator.SetBool("isCrouching", false);
            }
        }

        
        else
        {
            // ...reduce el tamaño del collider del personaje,
            col.size = new Vector2(col.size.x, 0.8f);

            // ...avisa al animador.
            animator.SetBool("isCrouching", true);
        }*/
        // Si el jugador se agacha...
        if (crouch)
        {
            // ...reduce el tamaño del collider del personaje,
            col.size = new Vector2(col.size.x, 0.8f);

            // ...avisa al animador.
            animator.SetBool("isCrouching", true);
        }
        else
        {
            // ...aumenta el tamaño del collider del personaje al original
            col.size = new Vector2(col.size.x, 1.6f);
            // ...avisa al animador.
            animator.SetBool("isCrouching", false);
        }
    }
}