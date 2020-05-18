using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const float playerMaxHP = 200f;
    float shieldMaxHP = 100f;
    float playerHP, shieldHP;       
    GameObject shield;       
    AudioManager audioManager;
    [SerializeField] UIManager UIManager;
    [SerializeField] GameObject player;   

    [SerializeField] Sound playerHit;
    private bool shieldBroken =false;
    //Si el jugador tiene una llave
    private bool hasKey=false;
    [SerializeField] Shield[] shieldArray;    

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
    }
    #endregion

    private void Start()
    {        
        shield = player.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        audioManager = AudioManager.instance;
        playerHP = playerMaxHP;
        shieldHP = shieldMaxHP;
        UIManager.UpdateHealthBar(playerMaxHP, playerHP);
        UIManager.UpdateShieldBar(shieldHP, shieldMaxHP);        
    }    

    public void SpawnPlayer(Vector2 point)
    {        
        playerHP = playerMaxHP;
        shieldHP = shieldMaxHP;
        player.transform.position = point;
        Debug.Log("El jugador ha spawneado en " + point + " con " + playerHP + " HP y " + shieldHP + " de escudo.");
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
        PlayerController pm = player.GetComponent<PlayerController>();
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

        else if (obj.GetComponent<PlayerController>()) 
        {
            UIManager.DamageOverlay();
            audioManager.PlaySoundOnce(playerHit);
            playerHP -= damage;

            if (playerHP < 0) 
            {
                SceneLoader.instance.ResetScene();
            }
            SlowMo(0.3f);
        }

        UIManager.UpdateShieldBar(shieldMaxHP, shieldHP);
        UIManager.UpdateHealthBar(playerMaxHP, playerHP);
    }    

    public bool ShieldBroken() 
    {
        return shieldBroken;
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
    
    private void SlowMo(float time)
    {
        Time.timeScale = 0.2f;
        Invoke("ReturnTimeScale", time * 0.2f);
    }

    private void ReturnTimeScale ()
    {
        Time.timeScale = 1f;
    }    

    public bool HasKey()
    {
        if (hasKey)
        {
            hasKey = false;
            return true;
        }
        else
            return false;
    }

    public void KeyPickup()
    {
        hasKey = true;
    }
}
