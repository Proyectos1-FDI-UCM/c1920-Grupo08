using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Dado que el script Explosion se encargará de todos los efectos físicos de la explosión, 
 el propósito de este script es activarlo cuando sea necesario. Este script funciona con ambos 
 tipos de mina*/

public class MinaProximidad : MonoBehaviour
{
    Explosion explosion;

    void Start()
    {
        explosion = GetComponentInChildren<Explosion>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (explosion != null)
            explosion.enabled = true;
    }
}