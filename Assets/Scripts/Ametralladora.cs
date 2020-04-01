using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ametralladora : MonoBehaviour
{
    public GameObject bullet;
    public GameObject spawn;
    public float tiempo;
    public float tiempobala;

    void Disparar()
    {
        Instantiate(bullet, spawn.transform.position, Quaternion.identity);
    }
    /*
    private void Update()
    {
        StartCoroutine(ExampleCoroutine());
    }
    IEnumerator ExampleCoroutine()
    {
        
        yield return new WaitForSeconds(2);
        tiempo -= Time.deltaTime;

        if (tiempo <= 0)
        {
            Disparar();

            tiempo = tiempobala;
        }

    }*/
    int i = 1;
    int j = 1;
    private void FixedUpdate()
    {
        if (i % 75 != 0)
            i++;
        else
        {
            j++;
            if (j % 10 == 0) Disparar();
            if (j % 30 == 0)
                i = j = 0;


        }
    }

}
