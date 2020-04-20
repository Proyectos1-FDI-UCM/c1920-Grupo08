using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    //Sprite del escudo
    Sprite sprite;
    //Valores de peso y resistencia
    public float weight = 1;
    public float maxhp = 100;
    //Placeholder para mostrar las estadísticas del escudo
    bool showinfo=false;

    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sprite = sr.sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        showinfo = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("col detected");
        //Si en cualquier momento en el que el jugador está en contacto con el escudo se pulsa
        //el botón de interactuar, cambiamos el escudo y destruimos el del suelo
        if (Input.GetButtonDown("Crouch"))
        {
            GameManager.instance.GetShield(maxhp, weight, sprite);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        showinfo = false;
    }
}
