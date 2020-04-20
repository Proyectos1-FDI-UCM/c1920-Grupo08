using UnityEngine;

public class GameManager : MonoBehaviour
{
    const float playerMaxHP = 200f;
    const float shieldMaxHP = 100f;
    float playerHP, shieldHP, shieldWeight; // Estado del juego
    float checkpointPlayerHP, checkpointShieldHP, checkpointShieldWeight; // Variables de puntos de guardados
    Vector2 lastCheckpoint; // Lugar donde reaparecerá el jugador al morir
    Sprite checkpointShield; // Escudo que tenía el jugador al pasar por el checkpoint
    GameObject player, shield;
    public static GameManager instance;
    bool isDead = false;
    const bool DEBUG = true;

    private bool isItPaused = false;

    // Almacena las frases para el sistema de diálogo
    private string[] frases = { "Texto por defecto, cambia el index", "Hola mi nombre es Ben, bienvenido!", "Seguramente necesite esto", "Mi escudo esta a punto de romperse!", "Necesito curar mis heridas", "༼ つ ◕_◕ ༽つ" };

    // Definir como único GameManager y designar quién será la UIManager
    UIManager UIManager;

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
        UIManager = GetComponent<UIManager>();
        UIManager.PauseMenu(isItPaused);
        playerHP = playerMaxHP;
        shieldHP = shieldMaxHP;
        shieldWeight = 0;

        UIManager.UpdateHeathBar(playerMaxHP, playerHP);
        UIManager.UpdateShieldBar(shieldHP, shieldMaxHP);
        UIManager.UpdateShieldHolder();
        // Dar valores a lastCheckpoint y a checkpointShield
    }

    private void Update()
    {
        if (Input.GetButtonDown("Escape")) 
        {
            if (isItPaused) 
            {
                isItPaused = false;
                UIManager.PauseMenu(isItPaused);
                Time.timeScale = 1f;                
            }

            else 
            {
                isItPaused = true;
                UIManager.PauseMenu(isItPaused);                
                Time.timeScale = 0f;
            }        
        }
    }

    public void SetPlayer(GameObject p)
    {
        player = p;
        shield = p.transform.GetChild(0).GetChild(0).gameObject;
    }


    public void GetShield(int healPoints, int weight) // Inicia los valores al coger un escudo
    {
        //shieldHP = healPoints;
        //shieldWeight = weight;
        // Llamar al UIManager
    }

    public void OnHit(GameObject obj, float damage) // Quita vida al jugador (colisión con enemigo)
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
                UIManager.UpdateHeathBar(playerMaxHP, playerHP);
            }
        }

        else if (obj.tag == "Player")
        {
            playerHP -= damage;
            // Llamar al UIManager
            UIManager.UpdateHeathBar(playerMaxHP, playerHP);
        }

        if (playerHP <= 0) OnDead(player);
    }

    public void OnHeal(int heal) // Cura al jugador (colision con botiquines)
    {
        if (playerHP < playerMaxHP)
        {
            if (playerHP + heal > playerMaxHP) playerHP = playerMaxHP;
            else playerHP += heal;
            // Llamar al UIManager
        }
    }

    public void Checkpoint(Vector2 pos, Sprite s) // Guarda los valores al pasar por un checkpoint 
    {
        lastCheckpoint = pos;
        checkpointPlayerHP = playerHP;
        checkpointShieldHP = shieldHP;
        checkpointShieldWeight = shieldWeight;
        checkpointShield = s;
    }

    public void OnDead(GameObject player) // Resetea desde el checkpoint
    {
        // Llamar a un método del jugador para que cambie a la posición de 
        //lastCheckpoint (enviada como parámetro) y le envíe el sprite del escudo
        Checkpoint(lastCheckpoint, checkpointShield);
        player.transform.position = lastCheckpoint;
    }

    public void OnDialogue(int index)
    {
        UIManager.Dialogue(frases[index]);
    }
}
