using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public string playScene;

   public void Play() 
    {
        if (playScene != null)
        {
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            Debug.Log("La escena no esta asignada");
        }
    }

    public void Exit() 
    {
        Debug.Log("El juego se ha cerrado");
        Application.Quit();    
    }
}
