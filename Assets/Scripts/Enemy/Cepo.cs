using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cepo : MonoBehaviour
{
    
    public float retencion;
    
    //public bool presionado;
    
   // Rigidbody2D rb2d;

    private void Start()
    {
        
        
    }
    /*private void Update()
    {
        while (retencion > 0 && presionado)
        {
            retencion -= Time.deltaTime;
            moving.Stunned();
            
        }
        if (retencion < 0f && presionado)
        {

            retencion = 0f;
            Destroy(this.gameObject);
        }
    }*/
    private void OnTriggerEnter2D(Collider2D player)
    {
        Stunned(player);
        Destroy(this.gameObject, retencion);
    }
    public void Stunned(Collider2D player)
    {

    }
}
