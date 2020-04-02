using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mina : MonoBehaviour
{
    public GameObject explosionEffect;
    public float radio = 30f;
    public float force = 50f;
    bool haExplotado = false;

    //Este metodo se activa cuando el jugador colisiona
    void OnCollisionEnter2D(Collision2D player)
    {
        if (haExplotado == false)
        {
            Explode();
            haExplotado = true;
        }
    }

    void Explode()
    {
        //Llama al prefab explosión
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        //Destruimos la mina y el prefab, y asignamos la variable true
        //para indicar que ha explotado
        Destroy(this.gameObject);
    }
}
