using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionKnockBack : MonoBehaviour
{
    public float knockBackForce;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(new Vector2((rb.position.x - transform.position.x) * knockBackForce, (rb.position.y - transform.position.y) * knockBackForce), ForceMode2D.Impulse);
        }
    }

}