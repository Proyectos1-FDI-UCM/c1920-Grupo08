using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cepo : MonoBehaviour
{
    
   
    public float retencion;
    //public bool presionado;
    PlayerMove moving;
    //Rigidbody2D rb2d;
    

    private void Start()
    {
       // presionado = false;
    }
    private void Update()
    {
        /*/Si el cepo es presionado el movimiento del jugador se detiene hasta que el contador llegue a cero
        if (presionado)
        {
            retencion -= Time.deltaTime;
            //rb2d.velocity = new Vector2(0f, 0f);
            //moving.Stunned();
            moving.enabled = false;
            if (retencion <= 0)
            {
                moving.enabled = true;
                
                retencion = 0;
                presionado = false;
                Destroy(this.gameObject);

            }
        }*/
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        moving.Stunned(retencion);
        moving = other.GetComponent<PlayerMove>();

        
        /*if (moving != null)
        {
            StartCoroutine(M());
        }*/
    }
   /* private IEnumerator M()
    {

        moving.Stunned();
        //desactiva el componente playermove
        yield return new WaitForSeconds(3f);
        
        Destroy(this.gameObject);

    }*/
   

   
}
