using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    [SerializeField] private GameObject logPrefab;
    [SerializeField] private float spawnTime;
    [SerializeField] private float warmup;
    [SerializeField] private bool left = false;
    [SerializeField] private float logSpeed;
    protected Transform spawnPool;
    private float moveX = 0;

    //Invoca un tronco cada X tiempo
    private void Start()
    {
        if (left)
        {
            moveX = -1;
        }
        else moveX = 1;

        InvokeRepeating("SpawnLog", 3f, spawnTime);
    }

    public void SpawnLog()
    {
        GameObject log = Instantiate(logPrefab, transform.position, transform.rotation, spawnPool);
        Rigidbody2D rb = log.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveX * logSpeed, rb.velocity.y);
    }
}
    

