using System.Collections;
using UnityEngine;

// Comunica el nuevo punto de reaparición para el jugador cuando este colisiona con él.
[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour
{   
    [SerializeField] Sound sound;    
    private string frase = "He alcanzado un nuevo punto de control";    
    private BoxCollider2D boxCollider;    

    private void Start()
    {        
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            SceneLoader.instance.SetSpawnPoint(transform.position);
            GameManager.instance.OnDialogue(frase);
            AudioManager.instance.PlaySoundOnce(sound);
            StartCoroutine(checkpointCD());
        }
    }

    IEnumerator checkpointCD() 
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(5f);
        boxCollider.enabled = true;
    }
}
