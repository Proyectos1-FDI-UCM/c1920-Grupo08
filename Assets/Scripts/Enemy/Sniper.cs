using TMPro;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class Sniper : MonoBehaviour
{
    // Dispara a un objetivo con una cadencia de disparo predeterminada y si está activado lo avisa mediante un láser
    // Usa Raycast para "simular impactos de objetos a alta velocidad"

    // El jugador no puede esquivar este enemigo, debe cubrirse con el escudo o con coberturas y esperar al enfriamiento para no recibir daño       

    [SerializeField] bool debug;

    [SerializeField] float damage; // Daño de disparo

    [SerializeField] float shotCD; // Tiempo entre cada disparo
    [SerializeField] float holdTime; // Tiempo que puede retener su punteria despues de perder de vista al objetivo
    [SerializeField] float decayTime; // Tiempo que tarda en perderse un apuntado completo
    [SerializeField] AnimationCurve laserIntensity; // Intensidad del láser en función de cuánto tiempo queda para el disparo

    float timeUntilShoot; // Tiempo restante para un apuntado completo
    float remainingHoldTime; // Tiempo restante para un apuntado completo
    bool decaying; // True si ha perdido de vista al objetivo, pero sigue vigilando su última posición vista
    
    [SerializeField] LayerMask targettingLayer; // Es importante separar las dos capas, porque el escudo debe bloquear el disparo pero no el rastreo
    [SerializeField] LayerMask bulletLayer;

    Vector2 hitPoint; // Punto de impacto

    [SerializeField] Transform rifle; // Sprite del fusil que se rota
    private SpriteRenderer rifleSprite;
    [SerializeField] Transform firePoint; // Punto de origen del disparo

    Transform target; // Objetivo
    CapsuleCollider2D targetCollider;

    [SerializeField] GameObject projectile; // Prefab de la bala

    [SerializeField] Sound shotSound;
    [SerializeField] Sound shieldHit;
    [SerializeField] Sound groundHit;
    private Color laserColour;
    private float laserWidth;

    AudioManager audioManager;

    LineRenderer laser; // Componente LineRenderer para el láser   

    Vector2 lastKnownLocation; // Última posición conocida del objetivo

    ContactFilter2D contactFilter;
    bool tracking = false;   // true as long as sniper is preparing to shoot
    bool inRange = false;   // true as long as sniper target is within trigger zone
    float range;
    RaycastHit2D rayCast;

    void Start()
    {
        laser = GetComponent<LineRenderer>();
        laserColour = laser.startColor;
        laserWidth = laser.startWidth;
        audioManager = AudioManager.instance;
        contactFilter.layerMask = targettingLayer;
        contactFilter.useLayerMask = true;
        // La máxima distancia posible a la que puede apuntar es la distancia entre el fusil y el centro de la zona de apuntado mas el radio de la zona
        range = (GetComponent<CircleCollider2D>().radius + Vector3.Magnitude(rifle.localPosition)) * 2f;
        timeUntilShoot = shotCD;
        target = GameManager.instance.player.transform;
        targetCollider = target.GetComponent<CapsuleCollider2D>();
        rifleSprite = rifle.GetComponent<SpriteRenderer>();
    }

    /*
     * If in range
     * Try to raycast at target, update lastKnownLocation
     * While tracking, aim at lastKnownLocation
     * After shooting, check for target location. If not found, set tracking to false.
     */

    private void Update()
    {
        if (tracking)
        {
            if (!decaying) timeUntilShoot -= Time.deltaTime;

            float animationProgress = laserIntensity.Evaluate((shotCD - timeUntilShoot) / shotCD);
            laser.endColor = new Color(laserColour.r, laserColour.g, laserColour.b, animationProgress);
            // Si está en el último 10% de la animación, hacemos parpadear el laser
            if (animationProgress > 0.90 && timeUntilShoot * 500f % 10 > 5 && decaying == false)
            {
                laser.startWidth = 0f; laser.endWidth = 0f;
            }   
            else
            {
                laser.startWidth = animationProgress * laserWidth;
                laser.endWidth = animationProgress * laserWidth * 5f;
            }
            if (debug) Debug.Log("Sniper aim progress: " + ((shotCD - timeUntilShoot) / shotCD));
            DrawLine(laser);

            // Rota su posición en dirección al objetivo
            Vector2 direction = lastKnownLocation - new Vector2(rifle.position.x, rifle.position.y);
            rifle.right = direction;
            rifleSprite.flipY = rifle.right.x < 0f;

            if (timeUntilShoot <= 0f)
                Shoot();
        }
        if (decaying && remainingHoldTime > 0.01f) // Si ha perdido de vista al objetivo, retiene su apuntado por holdTime
        {
            if (debug) Debug.Log("Sniper holding target. Remaining hold time:" + remainingHoldTime);
            remainingHoldTime -= Time.deltaTime;
        }
        else if (decaying && timeUntilShoot < shotCD)
        {
            if (debug) Debug.Log("Sniper aim decaying. Time until shoot: " + ((shotCD - timeUntilShoot) / shotCD));
            timeUntilShoot = Mathf.Clamp(timeUntilShoot + shotCD / decayTime * Time.deltaTime, 0, shotCD);
        }
        else if (decaying) 
        {
            if (debug) Debug.Log("Sniper target lost");
            decaying = false;
            tracking = false;
        }
    }

    void FixedUpdate()
    {
        if (inRange)
        {
            FindTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == target)
        {
            inRange = true;
            
            if (debug) Debug.Log("El jugador está en el alcance de " + this.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (debug) Debug.Log("El jugador ya no está en el alcance de " + this.gameObject.name);
        if (collision.GetComponent<PlayerController>() != null)
        {
            inRange = false;
            tracking = false;
            laser.enabled = false;
        }
    }

    bool FindTarget()
    {
        Vector2 targetPosition = new Vector2(target.position.x, target.position.y + targetCollider.offset.y);

        Vector2 direction = targetPosition - new Vector2(rifle.position.x, rifle.position.y);

        // RAYCAST
        rayCast = Physics2D.Raycast(rifle.position, direction, range, targettingLayer);

        if (rayCast.collider?.transform == target)
        {
            if (debug) Debug.Log("Target found");
            // Almacena el punto de impacto
            lastKnownLocation = rayCast.point;
            decaying = false;
            remainingHoldTime = holdTime;
            if (!tracking)
            {
                tracking = true;
                timeUntilShoot = shotCD;
                laser.enabled = true;
            }
            return true;
        }
        decaying = true;
        return false;
    }

    void Shoot() 
    {
        ///////////START HERE

        if (debug) Debug.Log("Shoot");

        // Reproduce el effecto de sonido
        audioManager.PlaySoundOnce(shotSound);

        Vector2 direction = lastKnownLocation - new Vector2(rifle.position.x, rifle.position.y);
        rayCast = Physics2D.Raycast(rifle.position, direction, range, bulletLayer);

        // Crea la bala
        CreateBullet();

        // Si el impactado es el player o el shield llama al GM y aplica daño
        if (rayCast.collider.GetComponent<PlayerController>() != null)
        {
            GameManager.instance.OnHit(rayCast.collider.gameObject, damage);
        }

        else if (rayCast.collider.GetComponent<ShieldClass>() != null)
        {
            GameManager.instance.OnHit(rayCast.collider.gameObject, damage);
            audioManager.PlaySoundOnce(shieldHit);
        }

        else
        {
            audioManager.PlaySoundOnce(groundHit);
        }

        // Aumenta el contador de disparo
        timeUntilShoot = shotCD;
        if (!FindTarget()) tracking = false;
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
        line.SetPosition(1, lastKnownLocation);
    }   
}
