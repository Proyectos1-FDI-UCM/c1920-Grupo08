using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayer : MonoBehaviour
{
    private GameManager gameManager;    

    void Awake()
    {        
        gameManager = GameManager.instance;
        //gameManager.SetPlayer(this.gameObject);
        //gameManager.SetShield(GetComponentInChildren<ShieldClass>().gameObject);
    }

    private void Start()
    {
        //gameManager.SpawnPlayer();
    }
}
