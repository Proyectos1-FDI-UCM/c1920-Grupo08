using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerJump : MonoBehaviour
{
    public float jumpForce; // Fuerza de salto

    private IsItGrounded isItGrounded; // Almacena el scrip para comprobar si esta en el suelo
    [SerializeField] private Sound sound;
    private bool jump;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isItGrounded = GetComponent<IsItGrounded>();
    }

    private void Update()
    {
        jump = Input.GetButtonDown("Jump");
    }

    void FixedUpdate()
    {
        //Solo saltamos si estamos en el suelo
        if (jump && isItGrounded.IsGrounded())
        {            
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            AudioManager.instance.PlaySoundOnce(sound);
            jump = false;
        }
    }
}