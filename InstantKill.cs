using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKill : MonoBehaviour
{
    public GameObject healthBar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        objectHealth hp = other.gameObject.GetComponent<objectHealth>();
        if (hp != null)
        {
            healthBar.GetComponent<valueBar>().SetValue(0);
            Destroy(other.gameObject);
        }

    }
}
