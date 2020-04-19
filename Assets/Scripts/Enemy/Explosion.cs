using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Este script se aplica a un objeto hijo de cualquier peligro que cause una explosión. 
 La razón de esto es para que el objeto padre pueda tener un collider propio sin interferir con el de 
 la explosión. Por ejemplo, una mina tiene un collider para su activación (la propia forma de la mina
 si es de contacto o un radio de detección si es de proximidad) y un hijo, el prefab Explosive, que
 tiene este script*/

public class Explosion : MonoBehaviour
{
    //El tiempo que pasa entre la activación de la componente y la explosión
    public float delay = 1f;
    //La velocidad de expansión de la onda explosiva 
    //CAUSA PROBLEMAS AL KNOCKBACK HORIZONTAL SI LA VELOCIDAD ES MENOR QUE 0.2
    public float explosionRate = 0.5f;
    //Radio máximo que alcanzará la explosión
    public float maxRadius = 10f;
    //Radio actual de la explosión
    float radius;
    //Fuerza de repulsión de la explosión
    public float force = 1f;
    //Daño máxmimo de la explosión
    public int explosionDamage;
    //Modificador horizontal de la fuerza
    public float horizontalFactor = 1;
    //Modificador vertical de la fuerza
    public float verticalFactor = 1;
    //true cuando la explosión haya comenzado
    bool exploded = false;
    //Este collider marca en cada momento el umbral de efecto de la explosión
    CircleCollider2D explosionRadius;
    //Si hay un BurstSpawner en el padre, lo activaremos en la explosión
    BurstSpawner burstSpawner;
    
    bool debug;

    void Start()
    {
        debug = false;
        radius = 0f;
        
        explosionRadius = gameObject.GetComponent<CircleCollider2D>();
        burstSpawner = transform.GetComponentInParent<BurstSpawner>();
    }

    void Update()
    {
        //Cuenta atrás hasta la explosión
        if (delay > 0)
            delay -= Time.deltaTime;
        else if (!exploded)
        {
            //Comienza la explosión
            if (debug)
                Debug.Log("Explosion started");
            exploded = true;
            //Si hay BurstSpawner, lo activamos
            if (burstSpawner != null)
                burstSpawner.enabled = true;
        }
    }

    void FixedUpdate()
    {
        //Aquí se realizan los efectos físicos de la explosión
        if (exploded == true)
        {
            //El radio de la explosión se expande
            if (radius < maxRadius)
            {
                radius += explosionRate;
            }
            //Si ya está al tamaño máximo, destruimos el objeto, dando fin a la explosión
            else
            {
                if (debug)
                    Debug.Log("Explosion ended");
                Object.Destroy(this.gameObject.transform.parent.gameObject);
            }
            //Actualizamos el radio del collider 
            explosionRadius.radius = radius;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //Si la explosión está en curso, todo objeto que entre en el collider y tenga un rigidbody será repelido
        if (exploded == true)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                if (debug)
                    Debug.Log("something got pushed");
                //La dirección del impulso será igual al vector posición del objetivo menos la posición de la bomba
                Vector2 target = collision.gameObject.transform.position;
                Vector2 bomb = gameObject.transform.position;
                Vector2 direction = target - bomb;
                direction.Normalize();

                //Aplicamos los modificadores de fuerza
                direction.x *= horizontalFactor;
                direction.y *= verticalFactor;
                rb.AddForce(10 * force * direction);

                //Hacemos menos daño mientras más se aleje el jugador del centro de la explosión
                    GameManager.instance.OnHit(collision.gameObject, (explosionDamage*(maxRadius-radius)));
            }
        }
    }
}
