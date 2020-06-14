using UnityEngine;

// Este script controlla la bala del tanque que simula un movimiento más parabólico
public class TankBullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    Vector2 direction;
    [SerializeField] float damage; 
    
    [SerializeField] Sound shieldHit;
    [SerializeField] Sound groundHit;

    AudioManager audioManager;
    
    void Awake()
    {
        direction = Vector2.left;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        audioManager = AudioManager.instance;
    }
    
    void FixedUpdate()
    {
        rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            GameManager.instance.OnHit(collision.gameObject, damage);
        }

        else if (collision.GetComponent<ShieldClass>() != null)
        {
            GameManager.instance.OnHit(collision.gameObject, damage);
            audioManager.PlaySoundOnce(shieldHit);
        }

        else
        {
            audioManager.PlaySoundOnce(groundHit);
        }        

        Destroy(this.gameObject);
    }
}
