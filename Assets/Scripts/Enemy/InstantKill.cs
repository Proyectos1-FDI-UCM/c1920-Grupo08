using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKill : MonoBehaviour
{
    public GameObject healthBar;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.GetComponent<Player>() != null)
        {
            GameManager.instance.OnHit(collision.gameObject, 1000f);
        }
    }
}
