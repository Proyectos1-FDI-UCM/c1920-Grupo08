using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Este script se encarga de actulizar la UI según le comunique el GM
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject shieldHolder, controlsMenu, pauseMenu, damageOverlay, dialogueBubble, blurShader;
    [SerializeField] TextMeshProUGUI bubbleText;
    [SerializeField] Slider healthS, shieldS, volumeSlider;
    [SerializeField] Sound buttonSound;

    AudioManager audioManager;
    string dialogue;
    float typeSpeed = 0.1f;
    bool isTalking = false;
    bool paused;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    void Start()
    {
        audioManager = AudioManager.instance;
        paused = false;
        pauseMenu.SetActive(paused);
        controlsMenu.SetActive(false);
        dialogueBubble.SetActive(false);
        damageOverlay.SetActive(false);
        blurShader.SetActive(false);
        isTalking = false;
        volumeSlider.value = SceneLoader.instance.CheckVolumeSlider();
    }

    // En update controlamos el uso del menú de pausa
    private void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            if (paused)
            {
                paused = false;
                blurShader.SetActive(false);
                pauseMenu.SetActive(false);
                controlsMenu.SetActive(false);
                Time.timeScale = 1f;
            }

            else
            {
                paused = true;
                blurShader.SetActive(true);
                pauseMenu.SetActive(true);
                controlsMenu.SetActive(false);
                Time.timeScale = 0f;
            }
        }
    }

    // Actualiza la barra de vida con los valores recibidos
    public void UpdateHealthBar(float maxHP, float currentHP)
    {
        healthS.maxValue = maxHP;

        healthS.value = currentHP;
    }

    // Actualiza la barra de resistencia con los valores recibidos
    public void UpdateShieldBar(float maxShield, float currentShield)
    {
        shieldS.maxValue = maxShield;

        shieldS.value = currentShield;
    }

    // Actualiza el icono del escudo
    public void UpdateShieldHolder(Sprite newShield)
    {
        shieldHolder.GetComponent<Image>().sprite = newShield;
    }

    // Muestra la frase recibida mediante un bocadillo de texto
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

    //Control de botones
    public void MainMenuButton()
    {
        audioManager.PlaySoundOnce(buttonSound);
        SceneLoader.instance.LoadMainMenu();
    }

    public void ExitButton()
    {
        audioManager.PlaySoundOnce(buttonSound);
        SceneLoader.instance.Exit();
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

    // Effecto de dao al recibir un impacto
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

    // Control de volumen
    public void VolumeSlider(float value)
    {
        SceneLoader.instance.SetVolume(value);
    }
}
