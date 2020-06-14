using System.Collections;
using UnityEngine;

// Este script de ametralladora completamente configurable en la cantidad de disparos que se quieren por ráfaga y lo enfriamientos entre ráfagas y entre disparos únicos
[RequireComponent(typeof(CircleCollider2D))]
public class MachineGun : MonoBehaviour
{    
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
    private Vector2 direction = Vector2.left;       
    private bool onRange = false;    

    private void Start()
    {
        firePoint = transform.GetChild(0).gameObject.GetComponent<Transform>();       
    }

    private void Update()
    {
        if (onRange) Shoot();
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
