using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] Sound sound;

    //Este script notifica al GM cuando el jugador recoge una llave
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("good");
        if (Input.GetButtonDown("Use"))
        {
            AudioManager.instance.PlaySoundOnce(sound);
            GameManager.instance.KeyPickup();
            Destroy(this.gameObject);
        }
    }
}
