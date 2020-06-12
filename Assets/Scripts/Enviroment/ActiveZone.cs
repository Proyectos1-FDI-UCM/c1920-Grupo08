using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script activa todos los objetos hijos del objeto asociado. Una vez activados,
//la zona deja de registrar colisiones
public class ActiveZone : MonoBehaviour
{
    BoxCollider2D col;
    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Pasamos por todos los hijos, activándolos
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(!transform.GetChild(i).gameObject.activeSelf);
        //Desactivamos el collider para evitar que se repita
        if (col != null)
            col.enabled = false;
    }
}
