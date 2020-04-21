using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    private int damage = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>() != null)
        {
            GameManager.instance.OnHit(collision.gameObject, damage);
        }

        if (collision.GetComponent<Shield>() != null)
        {
            GameManager.instance.OnHit(collision.gameObject, damage);
        }

        Destroy(this.gameObject);
    }

    public void SetDamage(int value) 
    {
        damage = value;
    }
}
