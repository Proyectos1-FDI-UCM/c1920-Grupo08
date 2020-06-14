using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Este es el único objeto/script que se mantiene durante todas las escenas es el encargado de los cambios y animaciones entre los niveles 
// así como de guardar los valores que deben retenerse durante la muerte del jugador y por tanto la recarga de la escena.
public class SceneLoader : MonoBehaviour
{
    
    [SerializeField] Animator faceToBlack;
    [SerializeField] AudioMixer audioMixer;

    private ShieldType currentShield = ShieldType.woodenPlank;
    private float volumeValue=1f;
    Vector2 lastCheckpoint = Vector2.zero;

    #region Singleton
    public static SceneLoader instance;    

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

        DontDestroyOnLoad(this);
    }
    #endregion   

    //Actualiza el punto de reaparición 
    public void SetSpawnPoint(Vector2 point) 
    {
        lastCheckpoint = point;
        Debug.Log("El nuevo spawn es: " + point);
    }    

    // Recarga la escena con la ayuda de una coroutina para hacer la animación y spawnear al player, normalmente se usa cuando el player muere.
    public void ResetScene() 
    {
        StartCoroutine(LoadThisScene());
    }

    IEnumerator LoadThisScene()
    {
        faceToBlack.SetTrigger("playEnd");
        yield return new WaitForSeconds(1f);
        yield return (SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
        GameManager.instance.SpawnPlayer(lastCheckpoint);
        faceToBlack.SetTrigger("playStart");
    }

    // Principalmente se usa en el cambio de niveles con la misma utilidad que el método anterior
    public void NextScene()
    {
        StartCoroutine(LoadNextScene());
        lastCheckpoint = Vector2.zero;
    }

    IEnumerator LoadNextScene()
    {
        faceToBlack.SetTrigger("playEnd");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       
        faceToBlack.SetTrigger("playStart");
    }

    // Este usa cuando quieres cargar una escena concreta, pero funciona igual que lo anteriores métodos
    public void SceneByIndex(int index)
    {
        StartCoroutine(LoadSceneByIndex(index));
        lastCheckpoint = Vector2.zero;
    }

    IEnumerator LoadSceneByIndex(int index)
    {
        faceToBlack.SetTrigger("playEnd");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
        faceToBlack.SetTrigger("playStart");
    }

    // Carga el menú y resetea el juego
    public void LoadMainMenu() 
    {
        Time.timeScale = 1f;
        lastCheckpoint = Vector2.zero;
        currentShield = ShieldType.woodenPlank;
        StartCoroutine(MainMenu());
    }

    IEnumerator MainMenu() 
    {
        faceToBlack.SetTrigger("playEnd");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
        faceToBlack.SetTrigger("playStart");
    }

    // Cierra la aplicación
    public void Exit()
    {
        Debug.Log("El juego se ha cerrado");
        Application.Quit();
    }

    // Para controlar el volumen durante los cambios y reseteos de escenas
    public float CheckVolumeSlider() 
    {
        return volumeValue;
    }
    public void SetVolume(float value)
    {
        volumeValue = value;
        audioMixer.SetFloat("GameVolume", Mathf.Log10(value) * 20);
    }

    // Para controlar el escudo actual durante los cambios y reseteos de escenas
    public ShieldType CheckShield()
    {
        return currentShield;
    }

    public void SetShield(ShieldType type) 
    {
        currentShield = type;
    }
}
