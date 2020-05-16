using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Sniper : MonoBehaviour
{
    // Dispara a un objetivo (no necesariamente al player) con una cadencia de disparo predeterminada y si esta activado lo avisa mediante un láser
    // Usa Raycast para "simular impactos de objetos a alta velocidad"

    // El jugador no puede esquivar este enemigo, debe cubrirse con el escudo o con coberturas y esperar al enfriamiento para no recibir daño

    // El objeto debe de tener un objeto vacio como hijo que indique el punto de origen del disparo (firePoint) y se debe asignar desde el editor
    // El objetivo debe de ser capaz de entrar en el rango de disparo en caso de no ser el jugador y ser un objeto inmóvil (range)
    // La bala debe ser un prefab vacio con un componente LineRenderer que se ocupa de como se va a visualizar la bala (projectile)
    // Para que el láser funcione el objeto debe tener un LineRenderer (laser)    

    [SerializeField] float range; // Rango de disparo

    [SerializeField] float damage; // Daño de disparo

    [SerializeField] float shotCD; // Tiempo entre cada disparo

    float elapsedTime = 0f; // Contador desde el último disparo

    [SerializeField] LayerMask targetLayer; // Máscara para comprobar que el objetivo esta en rango

    Vector2 hitPoint; // Punto de impacto

    [SerializeField] Transform firePoint; // Punto de origen del disparo

    Transform target; // Posición del objetivo

    [SerializeField] GameObject projectile; // Prefab de la bala

    [SerializeField] Sound shotSound;
    [SerializeField] Sound shieldHit;
    [SerializeField] Sound groundHit;

    AudioManager audioManager;

    LineRenderer laser; // Componente LineRenderer para el láser   

    Vector2 direction; // dirección de la bala

    void Awake()
    {
        laser = GetComponent<LineRenderer>();
    }

    void Start()
    {
        audioManager = AudioManager.instance;
    }

    void FixedUpdate()
    {
        // Si el objetivo esta en rango...
        if (Physics2D.OverlapCircle(firePoint.position, range, targetLayer))
        {
            // Almacenamos la posición del target
            Collider2D targetcollider = Physics2D.OverlapCircle(firePoint.position, range, targetLayer);

            target = targetcollider.gameObject.GetComponent<Transform>();

            laser.enabled = true;
            DrawLine(laser);

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
                Debug.Log(hit.collider.name);
                // Reproduce el effecto de sonido
                audioManager.PlaySoundOnce(shotSound);

                // Crea la bala
                createBullet();

                // Si el impactado es el player o el shield llama al GM y aplica daño
                if (hit.collider.GetComponent<Player>() != null)
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

        // Si el objetivo no esta en rango se apaga el láser
        else
        {
            laser.enabled = false;
        }
    }

    // Crea la bala y la dibuja
    void createBullet()
    {
        // Crea una bala 
        GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);

        // Dibuja el trazado de la bala
        LineRenderer tracer = bullet.GetComponent<LineRenderer>();
        DrawLine(tracer);

        // Destruye la bala rápidamente para crear sensación de movimiento
        Destroy(bullet, 0.02f);
    }

    // Dibuja una línea de una posición a otra utilizando el componente LineRenderer
    void DrawLine(LineRenderer line)
    {
        line.SetPosition(0, firePoint.position);
        line.SetPosition(1, hitPoint);
    }

    // Muestra el rango de disparo en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(firePoint.position, range);
    }
}
