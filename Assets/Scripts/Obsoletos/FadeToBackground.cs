
//Este script se aplica a todo lo que vaya a desvanecer después de impactar con algo, 
//como escombros y metralla

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBackground : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Accedemos a las componentes necesarias
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
        BoxCollider2D bc = this.gameObject.GetComponent<BoxCollider2D>();
        //Codigo defesivo
        if (rb != null && bc != null)
        {
            //Descativamos BoxCollider para que no interfiera con el movimiento del jugador ni se arrastre por
            //el movimiento de este
            bc.enabled = false;
            //Reducimos la velocidad del RigidBody y lo hacemos cinemático para darle el efecto de desvanecer 
            //por el suelo
            rb.velocity = new Vector2(rb.velocity.x, -1.0f);
            rb.isKinematic = true;
        }
        Destroy(this.gameObject, 1.5f);
    }
}
