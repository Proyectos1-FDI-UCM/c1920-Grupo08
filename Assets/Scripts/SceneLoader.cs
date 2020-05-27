using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    Vector2 lastCheckpoint = Vector2.zero;
    [SerializeField] Animator faceToBlack;
    [SerializeField] AudioMixer audioMixer;
    private float volumeValue=1f;
    
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

    public void SetSpawnPoint(Vector2 point) 
    {
        lastCheckpoint = point;
        Debug.Log("El nuevo spawn es: " + point);
    }    

    public void ResetScene() 
    {
        StartCoroutine(LoadThisScene());
    }

    IEnumerator LoadThisScene()
    {        
        yield return (SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
        GameManager.instance.SpawnPlayer(lastCheckpoint);        
    }

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

    public void LoadMainMenu() 
    {
        Time.timeScale = 1f;
        lastCheckpoint = Vector2.zero;
        StartCoroutine(MainMenu());
    }

    IEnumerator MainMenu() 
    {
        faceToBlack.SetTrigger("playEnd");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
        faceToBlack.SetTrigger("playStart");
    }

    public void Exit()
    {
        Debug.Log("El juego se ha cerrado");
        Application.Quit();
    }

    public float CheckVolumeSlider() 
    {
        return volumeValue;
    }
    public void SetVolume(float value)
    {
        volumeValue = value;
        audioMixer.SetFloat("GameVolume", Mathf.Log10(value) * 20);
    }
}
