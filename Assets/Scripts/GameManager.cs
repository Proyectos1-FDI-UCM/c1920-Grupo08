using UnityEngine;

public class GameManager : MonoBehaviour
{
    int playerHP, playerMaxHP, shieldHP, shieldWeight; // Estado del juego
    int checkpointPlayerHP, checkpointShieldHP, checkpointShieldWeight; // Variables de puntos de guardados
    Vector2 lastCheckpoint; // Lugar donde reaparecerá el jugador al morir
    Sprite checkpointShield; // Escudo que tenía el jugador al pasar por el checkpoint
    GameObject player, shield;
    public static GameManager instance;
    const bool DEBUG = true;
    // Definir como único GameManager y designar quién será la UIManager

    public bool Debug()
    {
        return DEBUG;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        playerHP = playerMaxHP = 100;
        shieldHP = 0;
        shieldWeight = 0;
        // Dar valores a lastCheckpoint y a checkpointShield
    }

    private void SetPlayer(GameObject p)
    {
        player = p;
        shield = p.transform.GetChild(0).GetChild(0).gameObject;
    }


    private void GetShield(int healPoints, int weight) // Inicia los valores al coger un escudo
    {
        shieldHP = healPoints;
        shieldWeight = weight;
        // Llamar al UIManager
    }

    private void OnHit(GameObject o, int damage) // Quita vida al jugador (colisión con enemigo)
    {
        if(o == shield) 
        {
            shieldHP -= damage;
            if (shieldHP < 0) playerHP += shieldHP; // Si el escudo queda con vida negativa, hace también daño al jugador
            // Llamar al UIManager
        }
        else if(o == player)
        {
            playerHP -= damage;
            // Llamar al UIManager
        }

        if (playerHP <= 0) OnDead();
    }

    private void OnHeal(int heal) // Cura al jugador (colision con botiquines)
    {
        if(playerHP < playerMaxHP)
        {
            if (playerHP + heal > playerMaxHP) playerHP = playerMaxHP;
            else playerHP += heal;
            // Llamar al UIManager
        }
    }

    private void Checkpoint(Vector2 pos, Sprite s) // Guarda los valores al pasar por un checkpoint 
    {
        lastCheckpoint = pos;
        checkpointPlayerHP = playerHP;
        checkpointShieldHP = shieldHP;
        checkpointShieldWeight = shieldWeight;
        checkpointShield = s;
    }

    private void OnDead() // Resetea desde el checkpoint
    {
// Llamar a un método del jugador para que cambie a la posición de lastCheckpoint (enviada como parámetro) y le envíe el sprite del escudo
        playerHP = checkpointPlayerHP;
        shieldHP = checkpointShieldHP;
        shieldWeight = checkpointShieldWeight;
    }
}
