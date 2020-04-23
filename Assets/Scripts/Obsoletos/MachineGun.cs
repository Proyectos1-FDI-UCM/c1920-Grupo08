using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    // Dispara a un objetivo (no necesariamente al player) en ráfagas de 3 disparos
    // Usa Raycast para "simular impactos de objetos a alta velocidad"

    // El jugador puede esquivar este enemigo, saltando la ráfaga, por ejemplo

    // El objeto debe de tener un primer objeto vacio como hijo que indique el punto de origen del disparo (firePoint) y se debe asignar desde el editor
    // El objeto debe de tener un segundo objeto vacío como hijo que indique la dirección de disparo (targetVector)
    // La bala debe ser un prefab vacio con un componente LineRenderer que se ocupa de como se va a visualizar la bala (projectile)
    // El objeto no dispara mientras que el un objeto con una capa definida este en rango (targetLayer, range)

    public float range = 30f; // Rango de detección, se usa para detectar si el player esta cerca

    public float damage = 10f; // Daño del burst

    public float shotCD = 0.2f; // Cooldown entre balas

    public float burstCD = 2f; // Cooldown entre ráfagas

    private float elapsedTime = 0; // Tiempo trascurrido desde la última ráfaga

    public LayerMask targetLayer; // Capa de detección, se usa para detectar si el player esta cerca

    private Vector2 hitPoint; // Punto de impacto

    private Transform targetVector; // Indicador de dirección

    public Transform firePoint; // Punto de origen del disparo   

    AudioSource sound; // Efecto de audio

    public GameObject projectile; // Bala    

    private float raycastR = 100f; // Rango de raycast

    private Vector2 direction; // dirección de la bala

    private void Start()
    {
        // Almacena el componenente de audio
        sound = GetComponent<AudioSource>();

        targetVector = this.gameObject.transform.GetChild(1).GetComponent<Transform>();

        // Rota su posición en dirección al objetivo
        direction = targetVector.position - transform.position;
        transform.right = direction;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Si el enemigo esta en rango...
        if (Physics2D.OverlapCircle(firePoint.position, range, targetLayer))
        {
            // RAYCAST
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, raycastR);

            // ALmacena el punto de impacto
            hitPoint = hit.point;

            // Dispara respetando la cadencia
            if (Time.time > elapsedTime)
            {
                // Si el impactado es el player o el shield llama al GM y aplica daño
                if (hit.collider.tag == "Player" || hit.collider.tag == "Shield")
                {
                    GameManager.instance.OnHit(hit.collider.gameObject, damage);
                }

                StartCoroutine(Burst());

                // Aumenta el contador de disparo
                elapsedTime = Time.time + burstCD;
            }
        }
    }

    // Crea un burst de 3 balas
    private IEnumerator Burst()
    {
        sound.Play();

        createBullet();
        yield return new WaitForSeconds(shotCD);

        createBullet();
        yield return new WaitForSeconds(shotCD);

        createBullet();
        yield return new WaitForSeconds(shotCD);
    }

    // Crea la bala y la dibuja
    private void createBullet()
    {
        // Crea una bala (un prefab vacio con un line renderer) 
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