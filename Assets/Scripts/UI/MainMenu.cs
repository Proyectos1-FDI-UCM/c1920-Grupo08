using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public string playScene;
   public GameObject mainMenu, controlsMenu;

    public void Start()
    {
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
            SceneManager.LoadScene(playScene);
        }
        else
        {
            Debug.Log("La escena no esta asignada");
        }
    }

    public void Controls() 
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void Back() 
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void Exit() 
    {
        Debug.Log("El juego se ha cerrado");
        Application.Quit();    
    }
}
