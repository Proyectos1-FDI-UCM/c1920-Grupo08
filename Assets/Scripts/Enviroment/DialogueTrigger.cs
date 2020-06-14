using UnityEngine;

// Este script se encarga de avisar al GM de una frase concreta que deba comunicarse al jugador por la interfaz cuando este colisiona con el
public class DialogueTrigger : MonoBehaviour
{    
    [SerializeField] private string frase;   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>()!=null)
        {            
           GameManager.instance.OnDialogue(frase);
           Destroy(this.gameObject);
        }
    }
}
