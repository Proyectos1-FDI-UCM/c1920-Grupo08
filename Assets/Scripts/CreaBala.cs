using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaBala : MonoBehaviour
{
    public GameObject bala;
    public Transform Target;
    Vector2 firepoint;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Dispara", 2f, 2f);

        firepoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void Dispara() 
    {
        Instantiate(bala);
        MoveTowards movetowards = bala.GetComponent<MoveTowards>();
        movetowards.target = Target;
        movetowards.firepoint = GetComponent<Transform>();
    }
}
