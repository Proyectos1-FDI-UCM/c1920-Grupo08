using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //Este script notifica al GM cuando el jugador recoge una llave
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("good");
        if (Input.GetButtonDown("Use"))
        {

            GameManager.instance.KeyPickup();
            Destroy(this.gameObject);
        }
    }
}
