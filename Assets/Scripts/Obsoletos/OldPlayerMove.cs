using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerMove : MonoBehaviour
{
    Rigidbody2D rb;    
    IsItGrounded isItGrounded;
    PlayerJump jumping;
    new AudioSource audio;

    Animator animator;
    //Velocidad estándar (máxima)
    [SerializeField]
    const float BASESPEED = 15;
    //Velocidad en cada momento
    float speed;
    //Multiplicador de velocidad agachado
    [SerializeField]
    [Range(0, 1)] 
    float crouchspeed;
    //Valor en porcentaje de cuanto queremos incrementar la velocidad cuando hacemos sprint (sprint CoD)
    //public float sprintBoost;
    //Duración del sprint (sprint dash)
    public float dashDur;
    //Valor en porcentaje de cuanto queremos incrementar la velocidad cuando hacemos sprint (sprint dash)
    public float dashSpeed;
    //Duración del enfriamiento del dash
    public float dashCDDur;
    //true si no puede moverse
    bool stunned;
    //duracion del stun
    public float stunCD;
    //true si dash está en enfriamiento
    bool dashCD;
    //true si dash está activo
    bool dash;
    //true si el dash activo es a la derecha
    bool dashRight;
    float acceleration = 1f;
    //Variable auxiliar para guardar el GetAxis horizontal
    float moveX;
    //Referencia al script agachar
    PlayerCrouch crouch;

    void Start()
    {
        speed = BASESPEED;
        audio = GetComponent<AudioSource>();  
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isItGrounded = GetComponent<IsItGrounded>();
        jumping = GetComponent<PlayerJump>();
        dashCD = false;
        dash = false;
        stunned = false;
        crouch = GetComponent<PlayerCrouch>();
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");

        if (Mathf.Abs(moveX)>0)
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

    //Movivmiento con sprint "dash"

    private void FixedUpdate()
    {
        //if (isItGrounded.IsGrounded())
        //    //No podemos hacer sprint si estamos agachados
        //    if (Input.GetButton("Sprint") && !dashCD && !Input.GetButton("Crouch"))
        //    {
        //        //Entra en modo dash
        //        dash = true;
        //        /*if (Input.GetAxisRaw("Horizontal") > 0)
        //            dashRight = true;
        //        else if (Input.GetAxisRaw("Horizontal") < 0)
        //            dashRight = false;*/
        //        //Cuando acabe el periodo dashDur, saldremos de modo dash
        //        Invoke("DashDuration", dashDur);
        //        //Ponemos el dash en enfriamiento
        //        dashCD = true;
        //        //Cuando acabe el tiempo dashCDDur, acaba el enfriamiento
        //        Invoke("DashCooldown", dashCDDur);
        //    }
        //Si estamos al lado de un muro, podemos controlar nuestro movimiento aunque no tengamos los pies en el suelo
        //De este modo podemos subir esquinas
        if (isItGrounded.IsGrounded()&& !stunned)
        {
            //Si nos encontramos en modo dash, nos movemos a la velocidad incrementada
            //if (dash)
            //    rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * (1 + dashSpeed / 100), rb.velocity.y);
            ////Si no, nos movemos a la velocidad normal
            //else
            //{                
            if (crouch.IsCrouched())
                rb.velocity = new Vector2(moveX * speed * crouchspeed, rb.velocity.y);
            else
                rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
            /*
            if (Mathf.Abs(rb.velocity.x) < speed)
            {                
                rb.AddForce(new Vector2(acceleration * moveX, 0));
            }*/

            animator.SetFloat("Speed", Mathf.Abs(moveX));
            //}
        }        
    }

    void DashDuration()
    {
        dash = false;
    }
    void DashCooldown()
    {
        dashCD = false;
    }
    public void Stunned(float time)
    {
        stunned = true;
        Invoke("StunCD", time);
        rb.velocity = Vector2.zero;
        jumping.enabled = false;
    }
    void StunCD()
    {
        stunned = false;
        jumping.enabled = true;
    }


    //El peso de los escudos escalará de 0 a 50, reduciendo la velocidad del jugador en un porcentaje igual al valor peso
    public void AddWeight(float weight)
    {
        if (weight <= 50 && weight >= 0)
            speed = BASESPEED * (1f - 0.01f * weight);
        else if (weight > 50)
            speed = BASESPEED / 2;
        else
            speed = BASESPEED;
    }
}