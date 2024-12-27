using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Sniper : MonoBehaviour
{
    // Dispara a un objetivo con una cadencia de disparo predeterminada y si esta activado lo avisa mediante un láser
    // Usa Raycast para "simular impactos de objetos a alta velocidad"

    // El jugador no puede esquivar este enemigo, debe cubrirse con el escudo o con coberturas y esperar al enfriamiento para no recibir daño       

    [SerializeField] bool debug;

    [SerializeField] float range; // Rango de disparo

    [SerializeField] float damage; // Daño de disparo

    [SerializeField] float shotCD; // Tiempo entre cada disparo

    float elapsedTime = 0f; // Contador desde el último disparo
    
    [SerializeField] LayerMask rayLayer;

    Vector2 hitPoint; // Punto de impacto

    [SerializeField] Transform firePoint; // Punto de origen del disparo

    Transform target; // Objetivo
    CapsuleCollider2D targetCollider;

    [SerializeField] GameObject projectile; // Prefab de la bala

    [SerializeField] Sound shotSound;
    [SerializeField] Sound shieldHit;
    [SerializeField] Sound groundHit;

    AudioManager audioManager;

    LineRenderer laser; // Componente LineRenderer para el láser   

    Vector2 direction; // dirección de la bala
    Vector2 targetPosition;

    ContactFilter2D contactFilter;
    bool onRange = false;
    RaycastHit2D hit;

    void Start()
    {
        laser = GetComponent<LineRenderer>();
        audioManager = AudioManager.instance;
        contactFilter.layerMask = rayLayer;
        contactFilter.useLayerMask = true;        
    }

    private void Update()
    {
        if (onRange)
        {
            laser.enabled = true;
            DrawLine(laser);

            targetPosition = new Vector2(target.position.x, target.position.y + targetCollider.offset.y);

            // Rota su posición en dirección al objetivo
            direction = targetPosition - new Vector2(transform.position.x, transform.position.y);
            transform.right = direction;           

            Shoot();        
        }

		else 
        {
            laser.enabled = false;
        }
    }

    void FixedUpdate()
    {
		if (onRange) 
        {
            // RAYCAST
            hit = Physics2D.Raycast(firePoint.position, direction, range, rayLayer);

            // Almacena el punto de impacto
            hitPoint = hit.point;
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            if (debug) Debug.Log("El jugador esta en rango de " + this.gameObject.name);
            elapsedTime = Time.time + shotCD;
            onRange = true;            
            target = collision.GetComponent<Transform>();
            targetCollider = collision.GetComponent<CapsuleCollider2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (debug) Debug.Log("El jugador ya no esta en rango de " + this.gameObject.name);
        if (collision.GetComponent<PlayerController>() != null)
        {
            onRange = false;            
        }
    }

    void Shoot() 
    {
        if (Time.time > elapsedTime)
        {
            // Reproduce el effecto de sonido
            audioManager.PlaySoundOnce(shotSound);

            // Crea la bala
            CreateBullet();

            // Si el impactado es el player o el shield llama al GM y aplica daño
            if (hit.collider.GetComponent<PlayerController>() != null)
            {
                GameManager.instance.OnHit(hit.collider.gameObject, damage);
            }

            else if (hit.collider.GetComponent<ShieldClass>() != null)
            {
                GameManager.instance.OnHit(hit.collider.gameObject, damage);
                audioManager.PlaySoundOnce(shieldHit);
            }

            else
            {
                audioManager.PlaySoundOnce(groundHit);
            }

            // Aumenta el contador de disparo
            elapsedTime = Time.time + shotCD;
        }
    }

    // Crea la bala y la dibuja
    void CreateBullet()
    {
        // Crea una bala 
        GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);

        // Dibuja el trazado de la bala
        LineRenderer tracer = bullet.GetComponent<LineRenderer>();
        DrawLine(tracer);

        // Destruye la bala rápidamente para crear sensación de movimiento
        Destroy(bullet, 0.025f);
    }

    // Dibuja una línea de una posición a otra utilizando el componente LineRenderer
    void DrawLine(LineRenderer line)
    {
        line.SetPosition(0, firePoint.position);
        line.SetPosition(1, hitPoint);
    }   
}
