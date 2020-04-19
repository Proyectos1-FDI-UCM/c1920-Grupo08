using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    public bool crouch;
    [Range(0, 1)] public float crouchspeed;
    public Collider2D crouchCol; // Collider al agacharse
    public Collider2D standCollider; // Collider original del personaje
    public PlayerMove playerMove;
    public LayerMask ground;
    float originalspeed;

    public Transform ceilingCheck; // Punto superior del personaje
    private float ceilingRadius = 0.24f; // Radio para el detector de techo

    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        float originalspeed = playerMove.speed;
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
                // ...reduce la velocidad,
                playerMove.speed *= crouchspeed;
            }

            // ... y no esta bloqueado por un techo.
            else
            {
                // ...activa el collider correspondiente para hacer mas grande al jugador.
                standCollider.enabled = true;
                crouchCol.enabled = false;
                // ...avisa al animador.
                animator.SetBool("isCrouching", false);
                playerMove.speed = originalspeed;
            }
        }

        // Si el jugador se agacha...
        else
        {
            // ...reduce la velocidad,
            playerMove.speed *= crouchspeed;

            // ...desactiva el collider correspondiente para hacer mas pequeño al jugador.
            standCollider.enabled = false;
            crouchCol.enabled = true;

            // ...avisa al animador.
            animator.SetBool("isCrouching", true);
        }
    }
}
