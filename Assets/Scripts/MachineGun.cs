using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{

    public Transform firePoint;

    public Transform target;

    public float range = 10f;

    public LayerMask targetLayer;

    public float shotCD = 0.5f;

    public float burstCD = 5f;

    Vector2 hitPoint;

    AudioSource sound;

    public GameObject projectile;

    float elapsedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {        
        if (Physics2D.OverlapCircle(firePoint.position, range, targetLayer))
        {
            // Rota su posición en dirección al objetivo
            Vector2 direction = target.position - transform.position;

            transform.right = direction;
            // RAYCAST
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, range);

            // ALmacena el punto de impacto
            hitPoint = hit.point;
            if (Time.time > elapsedTime)
            {
                StartCoroutine(Burst());

                // Aumenta el contador de disparo
                elapsedTime = Time.time + burstCD;
            }
        }
    }    

    IEnumerator Burst() 
    {
        yield return new WaitForSeconds(2f);

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
