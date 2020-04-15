//Este script se usa para todo objeto que hace daño al jugador o al escudo cuando colisiona con ellos

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class impactDamage : MonoBehaviour
{
    //Daño configurable desde el editor
    public int damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Accedemos a la vida del jugador/escudo para luego poder reducirla
        objectHealth hp = collision.gameObject.GetComponent<objectHealth>();
        //Codigo defensivo
        if (hp != null)
        {
            hp.applyDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
