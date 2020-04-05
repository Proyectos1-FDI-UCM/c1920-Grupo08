using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    // Dispara a un objetivo con una cadencia de disparo predeterminada y si esta activado lo avisa mediante un láser
    // Usa Raycast para "simular impactos de objetos a alta velocidad"
    // FALTA MEJORAR EL MÉTODO DE DIBUJADO DE LA BALA PARA QUE USE UN TRACE RENDERER EN LUGAR DE UN LINE REDERER

    public float range = 10f; // Rango de disparo

    public float damage = 10f; // Daño de disparo
    
    public float shotCD = 3f; // Tiempo entre cada disparo

    private float elapsedTime = 0f; // Contador desde el último disparo

    public LayerMask targetLayer; // Máscara para comprobar que el objetivo esta en rango

    private Vector2 hitPoint; // Punto de impacto

    public Transform firePoint; // Punto de origen del disparo

    public Transform target; // Posición del objetivo

    public GameObject projectile; // Prefab de la bala

    public bool laserSight = true; // Activar o desactivar el láser

    private LineRenderer laser; // Componente LineRenderer para el láser

    private AudioSource sound; //    

    void Start()
    {
        if (laserSight)
        {
            laser = GetComponent<LineRenderer>();
            sound = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Si el objetivo esta en rango...
        if (Physics2D.OverlapCircle(firePoint.position, range, targetLayer))
        {
            // Activa el láser
            if (laserSight)
            {
                laser.enabled = true;
                DrawLine(laser);
            }

            // Rota su posición en dirección al objetivo
            Vector2 direction = target.position - transform.position;

            transform.right = direction;

            // RAYCAST
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, range);

            // ALmacena el punto de impacto
            hitPoint = hit.point;           

            // Dispara respetando la cadencia de disparo
            if (Time.time > elapsedTime)
            {
                // Reproduce el effecto de sonido
                sound.Play();

                // Crea una bala (un prefab vacio con un line renderer) 
                GameObject Bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);

                // Dibuja el trazado de la bala
                LineRenderer tracer = Bullet.GetComponent<LineRenderer>();
                DrawLine(tracer);                

                // Si el impactado el el player o el shield llama al GM y aplica daño
                if (hit.collider.tag == "Player" || hit.collider.tag == "Shield")
                {
                    GameManager.instance.OnHit(hit.collider.gameObject, damage);
                }

                // Destruye la bala rapidamente para crear efecto de movimiento
                Destroy(Bullet, 0.10f);

                // Aumenta el contador de disparo
                elapsedTime = Time.time + shotCD;
            }
        }

        // Si el objetivo no esta en rango se apaga el láser
        else
        {
            if (laserSight)
            {
                laser.enabled = false;
            }
        }
    }  

    // Dibuja una línea de una posición a otra utilizando el componente LineRenderer
    void DrawLine(LineRenderer line)
    {
        line.SetPosition(0, firePoint.position);
        line.SetPosition(1, hitPoint);
    }

    // Muestra el rango de disparo en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(firePoint.position, range);
    }
}
