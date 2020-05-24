using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Log>() != null)
            Destroy(collision.gameObject);
    }
}
