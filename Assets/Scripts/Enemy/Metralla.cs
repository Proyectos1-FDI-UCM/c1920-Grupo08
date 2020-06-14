using UnityEngine;

// Este script de la metralla se encarga de elegir el sprite de la misma asi como de la rotación
public class Metralla : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Rotate rotate = GetComponent<Rotate>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = sprites[Random.Range(0, sprites.Length)];
        }
        if (rotate != null && rb != null)
        {
            if (rb.velocity.x < 0)
                rotate.RotateDirection('l');
            else
                rotate.RotateDirection('r');
        }
    }
}
