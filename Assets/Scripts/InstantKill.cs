using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKill : MonoBehaviour
{
    public GameObject healthBar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ObjectHealth hp = other.gameObject.GetComponent<ObjectHealth>();
        if (hp != null)
        {
            healthBar.GetComponent<ValueBar>().SetValue(0);
            Destroy(other.gameObject);
        }

    }
}
