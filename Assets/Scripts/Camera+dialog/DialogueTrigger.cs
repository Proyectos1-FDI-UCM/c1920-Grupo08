using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Consulta el index en el array del GM para elegir la frase")]
    public int index = 0;
    private int n = 0; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player") && (n < 1))
        {
            n += 1;
            GameManager.instance.OnDialogue(index);
            Destroy(this.gameObject);
        }
    }
}
