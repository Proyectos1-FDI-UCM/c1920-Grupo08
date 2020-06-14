using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKit : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            if (Input.GetButtonDown("Use"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
