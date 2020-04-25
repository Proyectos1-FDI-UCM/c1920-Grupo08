using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour
{   
    [SerializeField] private Sound sound;
    private string frase = "He alcanzado un nuevo punto de control";    
    private BoxCollider2D boxCollider;
    [SerializeField] bool isStartingPoint = false;

    private void Start()
    {
        if(isStartingPoint) GameManager.instance.SetSpawnPoint(transform.position);
        
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            GameManager.instance.SetSpawnPoint(transform.position);
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
