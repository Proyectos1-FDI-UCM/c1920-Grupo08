using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    Vector2 lastCheckpoint;
    
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

    private void Start()
    {
        lastCheckpoint = Vector2.zero;
    }    

    public void SetSpawnPoint(Vector2 point) 
    {
        lastCheckpoint = point;
        Debug.Log("El nuevo spawn es: " + point);
    }

    public Vector2 GetSpawnPoint() 
    {
        return lastCheckpoint;
    }

    public void ResetScene() 
    {
        StartCoroutine(LoadThisScene());
    }

    IEnumerator LoadThisScene()
    {
        yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        GameManager.instance.SpawnPlayer();
    }

    public void NextScene()
    {
        StartCoroutine(LoadNextScene());
        lastCheckpoint = Vector2.zero;
    }

    IEnumerator LoadNextScene()
    {
        yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.instance.SpawnPlayer();
    }

    public void LoadMainMenu() 
    {
        Time.timeScale = 1f;
        lastCheckpoint = Vector2.zero;
        StartCoroutine(MainMenu());
    }

    IEnumerator MainMenu() 
    {
        yield return SceneManager.LoadSceneAsync("00_MainMenu");
    }

    public void Exit()
    {
        Debug.Log("El juego se ha cerrado");
        Application.Quit();
    }
}
