using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    
    [SerializeField] private float heal = 100f;
    [SerializeField] private Sound sound; 

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>() != null)
        {
            if (Input.GetButtonDown("Use"))
            {
                AudioManager.instance.PlaySoundOnce(sound);
                GameManager.instance.OnHeal(heal);
                Destroy(this.gameObject);
            }
        }
    }
}
