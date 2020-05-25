using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metralla : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Rotate rotate = GetComponent<Rotate>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite=sprites[Random.Range(0, sprites.Length-1)];
        }
        if(rotate!=null && rb!= null)
        {
            if (rb.velocity.x < 0)
                rotate.RotateDirection('l');
            else
                rotate.RotateDirection('r');
        }
    }
}
