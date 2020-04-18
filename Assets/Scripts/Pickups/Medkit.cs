using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public float heal = 100f;
    private AudioSource sound;
    private int n = 0;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player") && (n < 1))
        {            
            n += 1;
            sound.Play();
            GameManager.instance.OnHeal(heal);
            Destroy(this.gameObject, 1f);
        }
    }
}
