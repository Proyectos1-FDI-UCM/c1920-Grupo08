using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firepoint;
    public Transform target;
    Vector2 targetv;
    Vector2 positionv;
    float speed = 50f;

    void Start()
    {
        targetv = target.position;
        positionv = firepoint.position;
        transform.position = positionv;
    }

    // Update is called once per frame
    void Update()
    {
        float fixedspeed= speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targetv, fixedspeed);
    }
}
