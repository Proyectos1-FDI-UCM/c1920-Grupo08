using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    Vector2 respawnPoint;
    void OnTriggerEnter2D(Collider2D other)
    {
        Checkpoint checkpoint = other.GetComponent<Checkpoint>();

        if (checkpoint != null)
            respawnPoint = other.transform.position;
    }

    void PlayerLostLife ()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.PlayerDied() == true)
                this.transform.position = respawnPoint;
        }
    }
}
