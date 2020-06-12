using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class MachineGun : MonoBehaviour
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
    [SerializeField] private GameObject bullet;
    [SerializeField] private int damagePerShot;
    [SerializeField] private Sound sound;

    [Header("Duración del ciclo de disparo")]
    [SerializeField] private float shot_CD = 1f;
    [SerializeField] private float burst_CD = 5f;
    [SerializeField] private int burst_count = 3;
    [SerializeField] private float bulletSpeed = 30f;

    private Transform firePoint;
    private float elapsedTime = 0f;
    private Vector2 direction = Vector2.zero;
    //private Transform target;   
    private bool onRange = false;
    private CircleCollider2D c_collider;

    private void Start()
    {
        firePoint = transform.GetChild(0).gameObject.GetComponent<Transform>();
        c_collider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (onRange)
        {           
            direction = Vector2.left;
            direction.Normalize();                    

            Shoot();
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            Debug.Log("El jugador esta en rango de " + this.gameObject.name);
            onRange = true;            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("El jugador ya no esta en rango de " + this.gameObject.name);
        if (collision.GetComponent<PlayerController>() != null)
        {
            onRange = false;           
        }
    }

    //Genera ráfagas según el cd de ráfaga (burst_CD)
    private void Shoot()
    {
        if (Time.time >= elapsedTime)
        {
            AudioManager.instance.PlaySoundOnce(sound);
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
}  
