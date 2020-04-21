using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Consulta el index en el array del GM para elegir la frase")]
    [SerializeField] private int index = 0;    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>()!=null)
        {            
            GameManager.instance.OnDialogue(index);
            Destroy(this.gameObject);
        }
    }
}
