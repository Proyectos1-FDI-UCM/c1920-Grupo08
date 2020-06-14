using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsItGrounded : MonoBehaviour
{
    [SerializeField] private LayerMask ground; // Layer para filtrar los objetos de tipo "suelo".

    [SerializeField] private Transform groundCheck; // Punto inferior del personaje

    private float groundRadius = 0.4f; // Radio para el detector de suelo
    
    //Radio para el detector de paredes. Mientras más grande sea, más fiable será el acto de subir esquinas, 
    //pero menos fiable serán los knockbacks
    //private float wallRadius = 0.4f;

    public bool IsGrounded()
    {
        //El método OverlapCircle hace una búsqueda de radio (2º parámetro) y centro (1er parámetro) buscando
        //cualquier objeto que colisione con la capa de colisión (3er parámetro), devuelve int el numero de
        //objetos encontrados
        return (Physics2D.OverlapCircle(groundCheck.position, groundRadius, ground) != null);
    }

    //public bool WallCheck()
    //{
    //    //Este método lo usaremos para detectar si estamos contra un muro para así habilitar el movimiento lateral
    //    //en el PlayerMove
    //    return (Physics2D.OverlapCircle(groundCheck.position, wallRadius, ground) != null);
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}