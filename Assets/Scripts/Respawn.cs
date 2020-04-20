using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    Vector2 respawnPoint;
    Sprite s;
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMove player = other.GetComponent<PlayerMove>();

        if (GameManager.instance != null && player != null)
        {
            respawnPoint = this.transform.position;

            GameManager.instance.Checkpoint(respawnPoint, s);
        }
    }
}

