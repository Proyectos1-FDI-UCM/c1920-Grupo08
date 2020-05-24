using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    enum Direction { Left, Right };
    [SerializeField]
    Direction direction;
    [SerializeField]
    float rotateSpeed;
    public void Update()
    {   
        if (direction==Direction.Right)
            transform.Rotate(0f, 0f, -rotateSpeed);
        else
            transform.Rotate(0f, 0f, rotateSpeed);
    }
}
