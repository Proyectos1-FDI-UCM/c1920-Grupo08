using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject shieldHolder, controlsMenu, pauseMenu, damageOverlay, dialogueBubble;
    [SerializeField] private TextMeshProUGUI bubbleText;
    private string dialogue;
    private float typeSpeed = 0.1f;
    [SerializeField] private Slider healthS, shieldS;
    bool paused;
    private AudioManager audioManager;
    [SerializeField] Sound buttonSound;
    private bool isTalking = false;

    private void Awake()
    {
        Time.timeScale = 1f;
        GameManager.instance.SetUIManager(this.gameObject);
    }
    void Start()
    {
        //GameManager.instance.SetUIManager(this.gameObject);
        audioManager = AudioManager.instance;
        paused = false;
        pauseMenu.SetActive(paused);
        controlsMenu.SetActive(false);
        dialogueBubble.SetActive(false);
        damageOverlay.SetActive(false);
        isTalking = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            if (paused)
            {
                paused = false;
                pauseMenu.SetActive(false);
                controlsMenu.SetActive(false);
                Time.timeScale = 1f;
            }

            else
            {
                paused = true;
                pauseMenu.SetActive(true);
                controlsMenu.SetActive(false);
                Time.timeScale = 0f;
            }
        }
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

    public void OnDialogue(string text)
    {
        if (!isTalking)
        {
            isTalking = true;
            dialogue = text;
            dialogueBubble.SetActive(true);
            bubbleText.text = "";
            StartCoroutine(Type());
        }
    }

    private IEnumerator Type()
    {
        foreach (char c in dialogue.ToCharArray())
        {
            bubbleText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        yield return new WaitForSeconds(1.5f);

        dialogueBubble.SetActive(false);

        isTalking = false;
    }

    public void MainMenuButton()
    {
        audioManager.PlaySoundOnce(buttonSound);
        GameManager.instance.MainMenu();
    }

    public void ExitButton() 
    {
        audioManager.PlaySoundOnce(buttonSound);
        GameManager.instance.Exit();
    }

    public void ControlsMenuButton() 
    {
        audioManager.PlaySoundOnce(buttonSound);
        controlsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void BackButton() 
    {
        audioManager.PlaySoundOnce(buttonSound);
        controlsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void DamageOverlay()    
    {
        StartCoroutine(DamageEffect());
    }

    IEnumerator DamageEffect() 
    {
        damageOverlay.SetActive(true);
        yield return null;
        damageOverlay.SetActive(false);
    }
}
