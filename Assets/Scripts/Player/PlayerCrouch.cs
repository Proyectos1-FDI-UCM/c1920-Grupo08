using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    public bool crouch;
    [Range(0, 1)] public float crouchspeed;
    public Collider2D crouchcol; // Collider al agacharse
    public Collider2D ogcollider; // Collider original del personaje

    public Transform ceilingCheck; // Punto superior del personaje
    private float ceilingRadius = 0.24f; // Radio para el detector de techo

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }
}
