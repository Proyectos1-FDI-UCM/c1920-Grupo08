using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
   public GameObject mainMenu, controlsMenu, title;
   AudioSource audioSource;
   

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (mainMenu != null)
        {
            mainMenu.SetActive(true);
        }
        else Debug.LogError("Añade el objeto mainMenu");

        if (controlsMenu != null)
        {
            controlsMenu.SetActive(false);
        }
        else Debug.LogError("Añade el objeto controlsMenu");        
    }
    public void Play()
    {
        audioSource.Play();
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Controls() 
    {
        audioSource.Play();
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void Back() 
    {
        audioSource.Play();
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void Exit() 
    {
        audioSource.Play();
        Debug.Log("El juego se ha cerrado");
        Application.Quit();    
    }
}
