using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [SerializeField] Transform pointA, pointB;
    float speed = 0.5f;
    Transform currentTarget;    
  
    void Update()
    {
        if (transform.position == pointA.position) currentTarget = pointB;
        if (transform.position == pointB.position) currentTarget = pointA;
        float movement = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, movement);
    }
}
