using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const float playerMaxHP = 200f;
    float shieldMaxHP = 100f;
    float playerHP, shieldHP;   
    Vector2 lastCheckpoint;
    GameObject player;
    GameObject shield;
    bool invulnerable=false;    
    AudioManager audioManager;
    private bool isItPaused = false;
    [SerializeField] Sound playerHit;
    private bool shieldBroken =false;

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
    public void SetStartingPoint(Vector2 point)
    {
        if (lastCheckpoint == Vector2.zero) 
        {
            lastCheckpoint = point;
        }
        Debug.Log("El spawn inicial de la escena es: " + point);
    }

    public void SetSpawnPoint(Vector2 point) 
    {
        lastCheckpoint = point;
        Debug.Log("El nuevo spawn es: " + point);
    }

    public void SetUIManager(GameObject obj)    
    {
        UIManager = obj.GetComponent<UIManager>();
        playerHP = playerMaxHP;
        shieldHP = shieldMaxHP;
        UIManager.UpdateHealthBar(playerMaxHP, playerHP);
        UIManager.UpdateShieldBar(shieldHP, shieldMaxHP);
    }

    public void SetShield (GameObject obj)
    {
        shield = obj;
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

    public void GetShield(ShieldType sType) // Inicia los valores al coger un escudo
    {
        int i = 0;
        //Buscamos el escudo recogido entre el array de escudos conocidos
        while (i < shieldArray.Length && shieldArray[i].shieldType != sType)
            i++;
        //Si no se encontró, no hacemos nada
        if (i == shieldArray.Length)
            return;
        //Actualizamos los PS del escudo, así como sus PS máximos
        shieldHP = shieldMaxHP = shieldArray[i].durability;
        //Actualizamos la velocidad del jugador según el peso del escudo nuevo
        PlayerMove pm = player.GetComponent<PlayerMove>();
        if (pm!=null)
            pm.AddWeight(shieldArray[i].weight);
        //Actualizamos el sprite y el icono del escudo
        UIManager.UpdateShieldHolder(shieldArray[i].sprite);
        shield.GetComponent<SpriteRenderer>().sprite = shieldArray[i].sprite;
    }

    public void OnHit(GameObject obj,float damage)
    {
        if (obj.GetComponent<ShieldClass>())
        {
            shieldHP -= damage;
            if (shieldHP <= 0)
            {
                shieldBroken = true;
                playerHP += shieldHP;
                audioManager.PlaySoundOnce(playerHit);                
            }
            SlowMo(0.1f);
        }

        else if (obj.GetComponent<Player>()) 
        {
            UIManager.DamageOverlay();
            audioManager.PlaySoundOnce(playerHit);
            playerHP -= damage;

            if (playerHP < 0) 
            {
                StartCoroutine(ResetScene());
            }
            SlowMo(0.3f);
        }

        UIManager.UpdateShieldBar(shieldMaxHP, shieldHP);
        UIManager.UpdateHealthBar(playerMaxHP, playerHP);
    }

    public void OnHitOld(GameObject obj, float damage) // Quita vida al jugador (colisión con enemigo)
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

                if (playerHP < 1f && damage < 200)
                    playerHP = 1f;

                if (damage > 10)
                {
                    invulnerable = true;
                    Invoke("invulnerabletimer", 0.2f);
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

    public bool ShieldBroken() 
    {
        return shieldBroken;
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
        shieldBroken = false;

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
    
    private void SlowMo(float time)
    {
        Time.timeScale = 0.2f;
        Invoke("ReturnTimeScale", time * 0.2f);
    }

    private void ReturnTimeScale ()
    {
        Time.timeScale = 1f;
    }
}
