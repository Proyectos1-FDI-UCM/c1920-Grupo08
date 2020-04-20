using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cepo : MonoBehaviour
{

    [SerializeField]
     float retencion;
   
    PlayerMove moving;
   
    

    void OnTriggerEnter2D(Collider2D other)
    {
        
        moving = other.GetComponent<PlayerMove>();
        moving.Stunned(retencion);
        Destroy(this.gameObject, retencion);

      
    }
  
}
