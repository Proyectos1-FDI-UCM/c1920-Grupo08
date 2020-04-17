using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject healthBar, shieldBar, shieldHolder, dialogueBubble,pauseMenu;
    public Text bubbleText;
    private string dialogue;
    private float typeSpeed= 0.05f;
    private Slider healthS, shieldS;    
    
    void Start()
    {
        dialogueBubble.SetActive(false);
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

    public void Dialogue(string text) 
    {
        dialogue = text;
        dialogueBubble.SetActive(true);
        bubbleText.text = "";
        StartCoroutine(Type());
    }

    private IEnumerator Type()
    {
        foreach (char c in dialogue.ToCharArray())
        {
            bubbleText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        yield return new WaitForSeconds(1f);

        dialogueBubble.SetActive(false);
    }

    public void PauseMenu(bool isItPaused) 
    {
        pauseMenu.SetActive(isItPaused);    
    }                 
}
