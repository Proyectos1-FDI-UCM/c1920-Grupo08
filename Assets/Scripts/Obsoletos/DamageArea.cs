using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    float time = 0f;
    public int damage;
    //Cuando el jugador este en el collider, el método se activa
    private void OnTriggerStay2D(Collider2D other)
    {
        //Con el transcurso de los segundos, se añade 1 al contador
        time += Time.deltaTime;
        ObjectHealth hp = other.gameObject.GetComponent<ObjectHealth>();
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        //Si el contador es mayor que 2, el objeto que ha impactado con el collider 
        //posee el elemento PlayerController y posee vida, entonces resta salud
        if (time>2 && hp != null && player != null)
        {
            hp.ApplyDamage(damage);
        }

    }
}
