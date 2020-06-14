using UnityEngine;

// Una pequeña animación reutilizable para los pickups.
public class PickupHover : MonoBehaviour
{
    float aux;
    float height;
    void Start()
    {
        aux = 0;
        height = transform.position.y;
    }

    void Update()
    {
        transform.Translate(0f, Mathf.Sin(aux)/90, 0f);
        aux += 0.06f;
    }
}
