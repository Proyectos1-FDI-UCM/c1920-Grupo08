using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    void Start()
    {        
        Destroy(this.gameObject, 2.9f);
    }

    public void Update()
    {        
        transform.Rotate(0f, 0f, -2f);
    }
  
}
