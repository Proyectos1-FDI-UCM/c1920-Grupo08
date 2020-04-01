using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsItGrounded : MonoBehaviour
{
    public LayerMask ground; // Layer para filtrar los objetos de tipo "suelo".

    public Transform groundCheck; // Punto inferior del personaje

    private float groundRadius = 0.1f; // Radio para el detector de suelo

    public bool IsGrounded()
    {
        //El método OverlapCircle hace una búsqueda de radio (2º parámetro) y centro (1er parámetro) buscando
        //cualquier objeto que colisione con la capa de colisión (3er parámetro), devuelve int el numero de
        //objetos encontrados
        return (Physics2D.OverlapCircle(groundCheck.position, groundRadius, ground) != null);
    }
}