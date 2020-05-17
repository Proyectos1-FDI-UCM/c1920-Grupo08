using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject[] Panels;
    AudioSource buttonSound;
    SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        buttonSound = GetComponent<AudioSource>();
        sceneLoader = SceneLoader.instance;
        SetPanel(0);
    }

    // Update is called once per frame
    public void Continue()
    {
        buttonSound.Play();
        SetPanel(1);
    }

    public void Play()
    {
        buttonSound.Play();
        sceneLoader.NextScene();
    }

    public void Controls()
    {
        buttonSound.Play();
        SetPanel(2);
    }

    public void Back()
    {
        buttonSound.Play();
        SetPanel(1);
    }

    public void Exit()
    {
        buttonSound.Play();
        sceneLoader.Exit();
    }

    void SetPanel(int index)
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            if (i == index)
            {
                Panels[i].SetActive(true);
            }
            else
            {
                Panels[i].SetActive(false);
            }
        }
    }
}
