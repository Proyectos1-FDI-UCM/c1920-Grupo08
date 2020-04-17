using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject healthBar, shieldBar, shieldHolder;
    private Slider healthS, shieldS;
    
    void Start()
    {
        healthS = healthBar.GetComponent<Slider>();
        shieldS = shieldBar.GetComponent<Slider>();
    }

    public void UpdateHealthBar(float maxHP, float currentHP)
    {       
        healthS.maxValue = maxHP;
        
        healthS.value = currentHP;
    }

    public void UpdateShieldBar(float maxShield, float currentShield)
    {        
        shieldS.maxValue = maxShield;
        
        shieldS.value = currentShield;
    }
    public void UpdateShieldHolder() { }
}
