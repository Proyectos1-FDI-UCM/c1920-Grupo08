using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{   
    //Placeholder para mostrar las estadísticas del escudo
    bool showinfo=false;
    [SerializeField] ShieldType shieldType;
    [SerializeField] Sound sound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        showinfo = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {        
        //Si en cualquier momento en el que el jugador está en contacto con el escudo se pulsa
        //el botón de interactuar, cambiamos el escudo y destruimos el del suelo
        if (Input.GetButtonDown("Use"))
        {
            AudioManager.instance.PlaySoundOnce(sound);
            GameManager.instance.GetShield(shieldType);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        showinfo = false;
    }
}
