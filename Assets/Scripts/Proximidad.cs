using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proximidad : MonoBehaviour
{
    public GameObject player;
    //public GameObject explosionEffect;
    public float Distancia;
    public float minDistancia;
    public float cooldown;
    
    //public Sprite minaActivada;
    SpriteRenderer spriteMina;
    
    void Update()
    {
        //Calcula la distancia entre la mina y player
        Distancia = Vector2.Distance(player.transform.position, transform.position);
        //Cuando player este a cierta distancia la mina se activa
        if (Distancia <= minDistancia)
        {
            spriteMina = GetComponent<SpriteRenderer>();
            spriteMina.material.SetColor("_Color", Color.red);
            //spriteMina.sprite = minaActivada;
            Debug.Log("minaActivada");
        }
        //Cuando player haya sobrepasado la distancia minima y se siga acercando se activara la explosion
        if (Distancia <= minDistancia/2)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0f)
            {
                cooldown = 0;
                //Explode();
            }
        }

    }
    /*void Explode()
    {

        //Llama al prefab explosión
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        //Destruimos la mina y el prefab, y asignamos la variable true
        //para indicar que ha explotado
        Destroy(this.gameObject);
    }*/
}
