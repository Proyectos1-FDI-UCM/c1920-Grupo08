using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public string playScene;
   public GameObject mainMenu, controlsMenu, title;
   [SerializeField] Sound buttonSound;
   private AudioManager audioManager;

    public void Start()
    {
        audioManager = AudioManager.instance;
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
        if (playScene != null)
        {
            audioManager.PlaySoundOnce(buttonSound);
            SceneManager.LoadScene(playScene);
        }
        else
        {
            Debug.Log("La escena no esta asignada");
        }
    }

    public void Controls() 
    {
        audioManager.PlaySoundOnce(buttonSound);
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void Back() 
    {
        audioManager.PlaySoundOnce(buttonSound);
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void Exit() 
    {
        audioManager.PlaySoundOnce(buttonSound);
        Debug.Log("El juego se ha cerrado");
        Application.Quit();    
    }
}
