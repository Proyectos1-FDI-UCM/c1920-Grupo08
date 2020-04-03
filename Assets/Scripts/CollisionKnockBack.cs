using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionKnockBack : MonoBehaviour
{
    //Módulo de la fuerza configurable desde el editor
    public float knockBackForce;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            //La dirección vendrá de la resta vectorial de las posiciones de los dos objetos
            //Hay que hacerlo por componentes porque el transform.position es un Vector3
            Vector2 direction = new Vector2((rb.position.x - transform.position.x), (rb.position.y - transform.position.y));
            //Normalizamos el vector dirección
            direction.Normalize();
            //Aplicamos la fuerza
            rb.AddForce(direction * knockBackForce, ForceMode2D.Impulse);
        }
    }
}