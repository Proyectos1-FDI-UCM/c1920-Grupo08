using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Sound shieldHit;
    [SerializeField] bool playGroundHitSound;
    [SerializeField] private Sound groundHit;
    [SerializeField] private int damage = 10;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            GameManager.instance.OnHit(collision.gameObject, damage);
        }

        else if (collision.GetComponent<ShieldClass>() != null)
        {
            GameManager.instance.OnHit(collision.gameObject, damage);
            audioManager.PlaySoundOnce(shieldHit);
        }

        else
        {
            if (playGroundHitSound) audioManager.PlaySoundOnce(groundHit);
        }

        Destroy(this.gameObject);
    }

    public void SetDamage(int value)
    {
        damage = value;
    }
}
