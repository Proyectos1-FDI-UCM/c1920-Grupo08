using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    [SerializeField] private LayerMask ladder;
    [SerializeField] private float climbSpeed;
    private Rigidbody2D rb;
    [SerializeField] private float distance;
    private float moveY;
    float gravity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, distance, ladder);
        
        if (hit.collider != null)
        {            
            moveY = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, moveY * climbSpeed * Time.fixedDeltaTime);
            rb.gravityScale = 0f;
        }

        else 
        {
            rb.gravityScale = gravity;
        }
    }
}
