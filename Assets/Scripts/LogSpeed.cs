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
    }

    //Mueve el tronco a la velocidad de "Speed" 
    void Update()
    {
        
    }

    
    public void Rotate(Vector2 rotation)
    {
        transform.right = rotation;
    }
}
