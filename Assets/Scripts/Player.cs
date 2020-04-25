using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gameManager;    

    void Awake()
    {        
        gameManager = GameManager.instance;
        gameManager.SetPlayer(this.gameObject);
    }

    private void Start()
    {
        gameManager.SpawnPlayer();
    }
}
