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
        SceneLoader.instance.NextScene();
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
        SceneLoader.instance.Exit();  
    }
}
