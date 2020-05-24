using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    enum Direction { Left, Right };
    [SerializeField]
    Direction direction;
    [SerializeField]
    float acceleration;
    [SerializeField]
    float maxSpeed;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Si el objeto no tiene Rigidbody, no va a funcionar
        if (rb == null)
            Destroy(this.gameObject);
        Destroy(this.gameObject, 10f);
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude < maxSpeed)
            if (direction == Direction.Right)
            {
                rb.AddForce(new Vector2(acceleration, 0));
            }
            else
            {
                rb.AddForce(new Vector2(-acceleration, 0));
            }
        else
            rb.velocity = rb.velocity / rb.velocity.magnitude * maxSpeed;
    }
}
