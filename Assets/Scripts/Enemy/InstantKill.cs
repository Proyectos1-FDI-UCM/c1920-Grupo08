using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKill : MonoBehaviour
{
    public GameObject healthBar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Creamos una variable referente al script PlayerMove
        PlayerMove player = other.GetComponent<PlayerMove>();

        //Si el objeto que colisiona, tiene PlayerMove...
        if (player != null)
        {
            //Establecemos la barra de salud a 0
            healthBar.GetComponent<ValueBar>().SetValue(0);

            //Llamamos al método de respawneo
            GameManager.instance.OnDead(player.gameObject);
        }
    }
}
