using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageOverTime : MonoBehaviour
{
    [SerializeField] private float damage;
    private float damageTick = 1f;
    private float timeElapsed = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            timeElapsed = Time.time;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((timeElapsed < Time.time) && (collision.GetComponent<PlayerController>() != null))
        {
            GameManager.instance.OnHit(collision.gameObject, damage);
            timeElapsed += damageTick;
        }
    }
}
