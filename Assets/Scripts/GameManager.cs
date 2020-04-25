using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const float playerMaxHP = 200f;
    float shieldMaxHP = 100f;
    float playerHP, shieldHP, shieldWeight;    
    Vector2 lastCheckpoint;
    GameObject player;
    //True cuando el jugador acaba de recibir daño y es brevemente inmune al daño
    bool invulnerable;    
    bool isDead = false;
    const bool DEBUG = true;
    AudioManager audioManager;
    private bool isItPaused = false;
    [SerializeField] Sound playerHit;

    [SerializeField] Shield[] shieldArray;
    
    private UIManager UIManager;

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private void Start()
    {
        audioManager = AudioManager.instance;
    }

    public void SetSpawnPoint(Vector2 point) 
    {
        lastCheckpoint = point;
        Debug.Log("El nuevo spawn es: " + point);
    }

    public void SetUIManager(GameObject obj)    
    {
        UIManager = obj.GetComponent<UIManager>();
        UIManager.UpdateHealthBar(playerMaxHP, playerHP);
        UIManager.UpdateShieldBar(shieldHP, shieldMaxHP);
    }

    public void SetPlayer(GameObject obj)
    {
        player = obj;        
    }

    public void SpawnPlayer() 
    {
        playerHP = playerMaxHP;
        shieldHP = shieldMaxHP;        
        player.transform.position = lastCheckpoint;
        Debug.Log("El jugador ha spawneado en " + lastCheckpoint + " con " + playerHP + " HP y " + shieldHP + " de escudo.");
    }

    public void GetShield(ShieldType shieldType) // Inicia los valores al coger un escudo
    {
        return;
    }

    public void OnHit(GameObject obj, float damage) // Quita vida al jugador (colisión con enemigo)
    {
        if (!invulnerable)
        {
            if (obj.tag == "Shield")
            {
                shieldHP -= damage;
                // Llamar al UIManager
                UIManager.UpdateShieldBar(shieldMaxHP, shieldHP);

                if (shieldHP < 0)
                {
                    playerHP += shieldHP; // Si el escudo queda con vida negativa, hace también daño al jugador
                                          // Llamar al UIManager

                    UIManager.UpdateHealthBar(playerMaxHP, playerHP);
                    UIManager.DamageOverlay();
                    audioManager.PlaySoundOnce(playerHit);
                }
                if (damage > 10)
                {
                    invulnerable = true;
                    Invoke("InvulnerableTimer", 0.2f);
                }
            }
            else if (obj.tag == "Player")
            {
                playerHP -= damage;
                // Llamar al UIManager
                UIManager.UpdateHealthBar(playerMaxHP, playerHP);
                UIManager.DamageOverlay();
                audioManager.PlaySoundOnce(playerHit);

                if (damage > 10)
                {
                    invulnerable = true;
                    Invoke("InvulnerableTimer", 0.2f);
                }
            }
        }
        else if (obj.tag == "Player")
        {
            playerHP -= damage;
            // Llamar al UIManager
            UIManager.UpdateHealthBar(playerMaxHP, playerHP);
            UIManager.DamageOverlay();
            audioManager.PlaySoundOnce(playerHit);
        }

        if (playerHP <= 0) StartCoroutine(ResetScene());
    }

    private void InvulnerableTimer()
    {
        invulnerable = false;
    }

    public void OnHeal(float heal) // Cura al jugador (colision con botiquines)
    {
        Debug.Log("Heal + " + heal);

        if (playerHP + heal > playerMaxHP)
        {
            playerHP = playerMaxHP;
        }

        else
        {
            playerHP += heal;
        }

        UIManager.UpdateHealthBar(playerMaxHP, playerHP);
    }

    public void OnRepair(float reapairvalue) // Cura al jugador (colision con botiquines)
    {
        Debug.Log("Repair + " + reapairvalue);

        if (shieldHP + reapairvalue > shieldMaxHP)
        {
            shieldHP = shieldMaxHP;
        }

        else
        {
            shieldHP += reapairvalue;
        }

        UIManager.UpdateShieldBar(shieldMaxHP, shieldHP);
    }

    public void OnDialogue(string frase) 
    {
        UIManager.OnDialogue(frase);
    }

    private IEnumerator ResetScene()
    {        
        yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);        
    }

    public void MainMenu() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("00_MainMenu");
    }

    public void Exit() 
    {
        Debug.Log("El juego se ha cerrado");
        Application.Quit();
    }
}
