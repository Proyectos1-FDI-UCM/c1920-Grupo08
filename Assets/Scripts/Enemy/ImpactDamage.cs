//Este script se usa para todo objeto que hace daño al jugador o al escudo cuando colisiona con ellos

using UnityEngine;
public class ImpactDamage : MonoBehaviour
{
    //Daño configurable desde el editor
    public int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.OnHit(collision.gameObject, damage);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.OnHit(collision.gameObject, damage);
    }
}
