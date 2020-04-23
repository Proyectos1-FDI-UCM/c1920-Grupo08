using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce; // Fuerza de salto

    private IsItGrounded isItGrounded; // Almacena el scrip para comprobar si esta en el suelo

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isItGrounded = GetComponent<IsItGrounded>();
    }
    void FixedUpdate()
    {
        //Solo saltamos si estamos en el suelo
        if (Input.GetButtonDown("Jump") && isItGrounded!=null && isItGrounded.WallCheck())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}