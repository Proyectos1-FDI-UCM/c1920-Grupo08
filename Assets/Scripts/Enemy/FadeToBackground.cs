//Este script se aplica a todo lo que vaya a desvanecer después de impactar con algo, 
//como escombros y metralla

using UnityEngine;

public class FadeToBackground : MonoBehaviour
{
    bool fading;
    SpriteRenderer sr;
    float aux;
    [SerializeField]
    float fadeDuration = 1f;

    private void Start()
    {
        if (fadeDuration <= 0f)
            Debug.LogError("FadeDuration must be a positive value.");
        aux = fadeDuration;
        fading = false;
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (fading)
        {
            aux -= Time.deltaTime;
            sr.color = new Color(1, 1, 1, (aux / fadeDuration));
            if (fadeDuration <= 0f)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Accedemos a las componentes necesarias
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
        BoxCollider2D bc = this.gameObject.GetComponent<BoxCollider2D>();
        //Codigo defesivo
        if (rb != null && bc != null)
        {
            //Descativamos BoxCollider para que no interfiera con el movimiento del jugador ni se arrastre por
            //el movimiento de este
            bc.enabled = false;
            //Reducimos la velocidad del RigidBody y lo hacemos cinemático para darle el efecto de desvanecer 
            //por el suelo
            rb.velocity = new Vector2(rb.velocity.x, -1.0f);
            rb.isKinematic = true;
        }
        fading = true;
    }
}
