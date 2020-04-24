using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{    
    [SerializeField] private Sound shieldHit;
    [SerializeField] private Sound groundHit;
    AudioManager audioManager;
    private int damage = 10;

    private void Start()
    {
        audioManager = AudioManager.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>() != null)
        {
            GameManager.instance.OnHit(collision.gameObject, damage);            
        }

        else if(collision.GetComponent<ShieldClass>() != null)
        {
            GameManager.instance.OnHit(collision.gameObject, damage);
            audioManager.PlaySoundOnce(shieldHit);
        }

        else 
        {
            audioManager.PlaySoundOnce(groundHit);
        }

        Destroy(this.gameObject);
    }

    public void SetDamage(int value) 
    {
        damage = value;
    }
}
