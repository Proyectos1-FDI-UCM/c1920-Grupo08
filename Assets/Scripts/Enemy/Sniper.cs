using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    // Dispara a un objetivo (no necesariamente al player) con una cadencia de disparo predeterminada y si esta activado lo avisa mediante un láser
    // Usa Raycast para "simular impactos de objetos a alta velocidad"

    // El jugador no puede esquivar este enemigo, debe cubrirse con el escudo o con coberturas y esperar al enfriamiento para no recibir daño

    // El objeto debe de tener un objeto vacio como hijo que indique el punto de origen del disparo (firePoint) y se debe asignar desde el editor
    // El objetivo debe de ser capaz de entrar en el rango de disparo en caso de no ser el jugador y ser un objeto inmóvil (range)
    // La bala debe ser un prefab vacio con un componente LineRenderer que se ocupa de como se va a visualizar la bala (projectile)
    // Para que el láser funcione el objeto debe tener un LineRenderer (laser)
    // Se puede usar para otro tipo de enemigo, por ejemplo, desactivando el láser desde el editor y disminuyendo el enfriamiento entre disparos.

    public float range = 10f; // Rango de disparo

    public float damage = 10f; // Daño de disparo

    public float shotCD = 3f; // Tiempo entre cada disparo

    private float elapsedTime = 0f; // Contador desde el último disparo

    public LayerMask targetLayer; // Máscara para comprobar que el objetivo esta en rango

    private Vector2 hitPoint; // Punto de impacto

    public Transform firePoint; // Punto de origen del disparo

    private Transform target; // Posición del objetivo

    public GameObject projectile; // Prefab de la bala

    public bool laserSight = true; // Activar o desactivar el láser

    private LineRenderer laser; // Componente LineRenderer para el láser

    private AudioSource sound; // Efecto de audio 

    private Vector2 direction; // dirección de la bala

    private void Start()
    {
        // Almacena el componenente de audio
        sound = GetComponent<AudioSource>();

        // Si el objeto usa láser almacena el componente LineRenderer que lo dibuja
        if (laserSight)
        {
            laser = GetComponent<LineRenderer>();
        }
    }

    private void FixedUpdate()
    {
        // Si el objetivo esta en rango...
        if (Physics2D.OverlapCircle(firePoint.position, range, targetLayer))
        {
            // Almacenamos la posición del target
            Collider2D targetcollider = Physics2D.OverlapCircle(firePoint.position, range, targetLayer);

            target = targetcollider.gameObject.GetComponent<Transform>();

            // Activa el láser
            if (laserSight)
            {
                laser.enabled = true;
                DrawLine(laser);
            }

            // Rota su posición en dirección al objetivo
            direction = target.position - transform.position;
            transform.right = direction;

            // RAYCAST
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, range);

            // Almacena el punto de impacto
            hitPoint = hit.point;

            // Dispara respetando la cadencia de disparo
            if (Time.time > elapsedTime)
            {
                // Reproduce el effecto de sonido
                sound.Play();

                // Crea la bala
                createBullet();

                // Si el impactado es el player o el shield llama al GM y aplica daño
                if (hit.collider.tag == "Player" || hit.collider.tag == "Shield")
                {
                    GameManager.instance.OnHit(hit.collider.gameObject, damage);
                }

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

    // Crea la bala y la dibuja
    private void createBullet()
    {
        // Crea una bala 
        GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);

        // Dibuja el trazado de la bala
        LineRenderer tracer = bullet.GetComponent<LineRenderer>();
        DrawLine(tracer);

        // Destruye la bala rápidamente para crear sensación de movimiento
        Destroy(bullet, 0.08f);
    }

    // Dibuja una línea de una posición a otra utilizando el componente LineRenderer
    private void DrawLine(LineRenderer line)
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
