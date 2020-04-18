using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    public float lifetime;

    /*void Start()
    {
        Invoke("DestroyTime", lifetime);
    }*/

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Bala") 
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    /*void DestroyTime()
    {
        //Aqui meteriamos un instantiate para el efecto de destrucción
        Destroy(gameObject);
    }*/
}
