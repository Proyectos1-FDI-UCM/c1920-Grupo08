using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    //Sprite del escudo
    public Sprite shield;
    //Valores de peso y resistencia
    public float weight = 1;
    public float maxhp = 100;
    //Placeholder para mostrar las estadísticas del escudo
    bool showinfo=false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        showinfo = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Si en cualquier momento en el que el jugador está en contacto con el escudo se pulsa
        //el botón de interactuar, cambiamos el escudo y destruimos el del suelo
        if (Input.GetButtonDown("Crouch"))
        {
            GameManager.instance.GetShield(maxhp, weight);
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        showinfo = false;
    }
}
