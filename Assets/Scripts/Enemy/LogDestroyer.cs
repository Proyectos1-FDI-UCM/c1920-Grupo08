using UnityEngine;

// Este script destruye los troncos al entrar en su collider
public class LogDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Log>() != null)
            Destroy(collision.gameObject);
    }
}
