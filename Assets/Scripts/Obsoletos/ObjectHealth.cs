using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public GameObject healthBar;

    public int health = 100;
    
    void Start()
    {
        
        healthBar.GetComponent<ValueBar>().SetMaxValue(health);
    }

    public void ApplyDamage(int damage) 
    {
        health = health - damage;
        healthBar.GetComponent<ValueBar>().SetValue(health);
    }
}
