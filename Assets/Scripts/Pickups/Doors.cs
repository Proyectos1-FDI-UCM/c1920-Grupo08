using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Doors : MonoBehaviour
{
    [SerializeField]
    bool locked=false;
    BoxCollider2D bc;
    private void Start()
    {
        bc = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>() != null)
        {
            if (Input.GetButtonDown("Use") && (!locked || GameManager.instance.HasKey()))
            {
                bc.enabled = false;
                //Change sprite
            }
        }
    }
}
