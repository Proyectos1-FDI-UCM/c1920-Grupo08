using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Consulta el index en el array del GM para elegir la frase")]
    public int index = 0; 
    public LayerMask s;
    public float range = 0.5f;
    private void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(transform.position, range, s))
        {
            GameManager.instance.OnDialogue(index);
            Destroy(this.gameObject);
        }
    }

    // Muestra el rango de disparo en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
