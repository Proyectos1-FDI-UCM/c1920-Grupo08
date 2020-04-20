using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpeed : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(this.gameObject, 3f);
    }

    //Mueve el tronco a la velocidad de "Speed" 

    public void Rotate(Vector2 rotation)
    {
        transform.right = rotation;
    }
}
