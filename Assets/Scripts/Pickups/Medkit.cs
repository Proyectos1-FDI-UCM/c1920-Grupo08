using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    
    [SerializeField] private float heal = 100f;
    private AudioSource sound;    

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>()!=null)
        {           
            sound.Play();
            GameManager.instance.OnHeal(heal);
            Destroy(this.gameObject, 1f);
        }
    }
}
