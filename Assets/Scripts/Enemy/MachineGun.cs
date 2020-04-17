using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    // Dispara a un objetivo (no necesariamente al player) en ráfagas de 3 disparos
    // Usa Raycast para "simular impactos de objetos a alta velocidad"
    // FALTA MEJORAR EL MÉTODO DE DIBUJADO DE LA BALA PARA QUE USE UN TRACE RENDERER EN LUGAR DE UN LINE REDERER

    public Transform firePoint; // Punto de origen del disparo

    public Transform target; // Posición del target

    public float range = 10f; // Rango de disparo

    public LayerMask targetLayer; // Capa en la que se encuentra el objetivo

    public float shotCD = 0.2f; // Cooldown entre balas

    public float burstCD = 2f; // Cooldown entre ráfagas

    Vector2 hitPoint; // Punto de impacto

    AudioSource sound; // Efecto de audio

    public GameObject projectile; // Bala

    float elapsedTime = 0; // Tiempo trascurrido desde la última ráfaga

    public float damage = 10f; // Daño de disparo

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {   
        // Si el enemigo esta en rango...
        if (Physics2D.OverlapCircle(firePoint.position, range, targetLayer))
        {
            // Rota su posición en dirección al objetivo
            Vector2 direction = target.position - transform.position;

            transform.right = direction;
            // RAYCAST
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, range);

            // ALmacena el punto de impacto
            hitPoint = hit.point;

            // Dispara respetando la cadencia
            if (Time.time > elapsedTime)
            {
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

    IEnumerator Burst() 
    {
        sound.Play();

        createBullet();
        yield return new WaitForSeconds(shotCD);
        
        createBullet();
        yield return new WaitForSeconds(shotCD);
        
        createBullet();
        yield return new WaitForSeconds(shotCD);        
    }

    void createBullet() 
    {
        // Crea una bala (un prefab vacio con un line renderer) 
        GameObject Bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);

        // Dibuja el trazado de la bala
        LineRenderer tracer = Bullet.GetComponent<LineRenderer>();

        DrawLine(tracer);

        Destroy(Bullet, 0.08f);
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
