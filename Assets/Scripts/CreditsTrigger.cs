using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsTrigger : MonoBehaviour
{
    [SerializeField] GameObject credits;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null) 
        {
            credits.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
