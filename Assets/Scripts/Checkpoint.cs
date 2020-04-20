
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    bool checkpointActivated = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMove player = other.GetComponent<PlayerMove>();

        if (player != null)
        {
            checkpointActivated = true;
        }

        Debug.Log("Checkpoint: " + checkpointActivated);
    }
}
