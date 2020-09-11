using UnityEngine;

public class Helicopter : MonoBehaviour
{
    Transform helicopter;
    Transform shotPoint;
    Rigidbody2D rb;
    Vector2 speed = new Vector2(12, 0);
    public GameObject bullet;
    int time = 1; // Variable para calcular el tiempo entre disparos

    void Start()
    {
        helicopter = gameObject.transform;
        shotPoint = gameObject.GetComponentInChildren<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = speed;
    }

    private void OnCollisionEnter2D(Collision2D obj)
    {
        // Cambia el sentido del helicóptero
        helicopter.localScale = new Vector3(-helicopter.localScale.x, helicopter.localScale.y, helicopter.localScale.z);
        speed.x = -speed.x;
        rb.velocity = speed;

        // Mueve el límite al otro extremo del recorrido del helicóptero
        Vector3 limite = obj.gameObject.transform.position;
        if (limite.x == 60) limite.x = 16;
        else limite.x = 60;
        obj.gameObject.transform.position = limite;
    }
    
    private void FixedUpdate()
    {
        if (gameObject.activeInHierarchy)
        {
            //Debug.Log("Helicoptero activo");
            time++;
            if (time % 5 == 0)
            {
                //Debug.Log("Disparo");
                time = 1;
                GameObject bullet1 = Instantiate(bullet, shotPoint);
                Rigidbody2D bulletRB = bullet1.GetComponent<Rigidbody2D>();
                bullet1.GetComponent<Bullet>().SetDamage(30);
                bulletRB.velocity = new Vector2(0, -10);
                bullet1.transform.parent = null; // Para que no se teletransporten al girar el helicóptero
            }
        }
    }
    
}
