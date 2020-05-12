using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>() != null)
        {
            if (Input.GetButtonDown("Use"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
