using UnityEngine;

public class ShieldPickup : MonoBehaviour
{    
    [SerializeField] ShieldType shieldType;
    [SerializeField] Sound sound;   

    private void OnTriggerStay2D(Collider2D collision)
    {        
        //Si en cualquier momento en el que el jugador está en contacto con el escudo se pulsa
        //el botón de interactuar, cambiamos el escudo y destruimos el del suelo
        if (Input.GetButtonDown("Use"))
        {
            AudioManager.instance.PlaySoundOnce(sound);
            GameManager.instance.GetShield(shieldType);
            GameManager.instance.OnDialogue("Esto tiene buena pinta...");
            Destroy(this.gameObject);
        }
    } 
}
