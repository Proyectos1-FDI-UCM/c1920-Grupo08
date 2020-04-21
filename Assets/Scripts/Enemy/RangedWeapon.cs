using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class RangedWeapon : MonoBehaviour
{
    // Scrip configurable para el uso en armas a distancia
    // Dos modos de disparo principales estático o de seguimientoa un target (staticShooting)
    // Posibilidad de añadir un designador láser (laserSight + LineRenderer)
    // Completamente configurable en la cantidad de disparos que se quieren por ráfaga y lo enfriamientos entre ráfagas y entre disparos únicos

    // Para funcionar correctamente el objeto necesita un punto de origen de disparo (firePoint) que se debe asignar desde el editor
    // La bala debe contener un RigidBody dinámico sin gravedad
    // Para el rango de detección es necesario añadir un CircleCollider2D
    // Para el láser el objeto necesita un LineRenderer y configurar correctamente el targetLayer

    [Header("Configuración")]
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private int damagePerShot;

    [Header("Duración del ciclo de disparo")]
    [SerializeField] private float shot_CD = 1f;
    [SerializeField] private float burst_CD = 5f;
    [SerializeField] private int burst_count = 3;
    

    [Header("Tipo de disparo")]
    [SerializeField] private bool laserSight = false;        
    [SerializeField] private bool staticShooting = false;

    [SerializeField] private float bulletSpeed = 30f;

    private float elapsedTime = 0f;
    private Vector2 direction = Vector2.zero;
    private Transform target;
    private LineRenderer laser;
    private bool onRange = false;
    private CircleCollider2D c_collider;
    private new AudioSource audio;

    private void Start()
    {
        if (laserSight)
        {
            laser = GetComponent<LineRenderer>();
            if (laser == null) 
            {
                Debug.Log("Para que " + this.gameObject.name + " pueda usar láser necesita un LineRenderer");
                laserSight = false;
            }
        }

        c_collider = GetComponent<CircleCollider2D>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (onRange)
        {
            direction = target.position - transform.position;
            direction.Normalize();
            
            if (!staticShooting)
            {
                transform.right = direction;
            }
            if (laserSight)
            {
                laser.enabled = true;
                DrawLine(laser);
            }

            Shoot();
        }
        else 
        {
            if (laserSight) laser.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>() != null)
        {
            Debug.Log("El jugador esta en rango de " + this.gameObject.name);
            if (staticShooting)
            {
                target = firePoint;
            }
            else
            {
                target = collision.GetComponent<Transform>();
            }

            onRange = true;
            if (laserSight) laser.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("El jugador ya no esta en rango de " + this.gameObject.name);
        if (collision.GetComponent<PlayerMove>() != null)
        {
            onRange = false;
            if (laserSight) laser.enabled = false;
        }
    }

    //Genera ráfagas según el cd de ráfaga (burst_CD)
    private void Shoot()
    {
        if (Time.time >= elapsedTime)
        {
            audio.Play();
            StartCoroutine(Burst());
            elapsedTime = Time.time + burst_CD + shot_CD * burst_count;
        }
    }

    // Genera los diparos según el cd de disparo (shot_CD)
    private IEnumerator Burst()
    {
        for (int i = 0; i < burst_count; i++)
        {
            SpawnBullet();
            yield return new WaitForSeconds(shot_CD);
        }
    }

    // Instancia la bala y aplica velocidad
    private void SpawnBullet()
    {
        GameObject m_bullet = Instantiate(bullet, firePoint.position, transform.rotation);
        Rigidbody2D rb = m_bullet.GetComponent<Rigidbody2D>();
        m_bullet.GetComponent<Bullet>().SetDamage(damagePerShot);
        rb.velocity = (direction * bulletSpeed);
    }    

    // Dibuja una línea de un punto a otro (LineRenderer)
    private void DrawLine(LineRenderer line)
    {
        RaycastHit2D hitPoint = Physics2D.Raycast(firePoint.position, direction, c_collider.radius, targetLayer);
        line.SetPosition(0, firePoint.position);
        line.SetPosition(1, hitPoint.point);
    }
}
