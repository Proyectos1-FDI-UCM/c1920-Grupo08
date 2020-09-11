﻿using UnityEngine;
using UnityEngine.UI;

// Este script se encarga de navegar mediante los paneles del menú principal y comunicar los cambios de escena de sus botones.

[RequireComponent(typeof(AudioSource))]
public class MenuManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] GameObject[] Panels;
    AudioSource buttonSound;
    SceneLoader sceneLoader;

    void Start()
    {
        buttonSound = GetComponent<AudioSource>();
        sceneLoader = SceneLoader.instance;
        volumeSlider.value = sceneLoader.CheckVolumeSlider();
        SetPanel(0);
    }

    public void Play()
    {
        buttonSound.Play();
        SetPanel(3);
    }

    public void Tutorial(int index)
    {
        buttonSound.Play();
        sceneLoader.SceneByIndex(index);
    }

    public void Extra()
    {
        buttonSound.Play();
        //SetPanel(3);
        sceneLoader.SceneByIndex(6);
    }

    public void SceneByIndex(int index)
    {
        sceneLoader.SceneByIndex(index);
    }

    public void Controls()
    {
        buttonSound.Play();
        SetPanel(1);
    }

    public void Credits()
    {
        SetPanel(2);
    }

    public void Back()
    {
        buttonSound.Play();
        SetPanel(0);
    }

    public void Exit()
    {
        buttonSound.Play();
        sceneLoader.Exit();
    }

    public void VolumeSlider(float value)
    {
        sceneLoader.SetVolume(value);
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
