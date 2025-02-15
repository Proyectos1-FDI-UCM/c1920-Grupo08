using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public enum Languages { Esp, Eng };

    public static Languages currLanguage
    {
        private set;

        get;
    }

    private List<Translatable> translatables;

    private void Awake()
    {
        translatables = new List<Translatable>();
    }

    
    public void AddTranslatable(Translatable t)
    {
        translatables.Add(t);
    }

    public void RemoveTranslatable(Translatable t)
    {
        translatables.Remove(t);
    }

    public void ChangeLanguage()
    {
        Debug.Log("Change language");
        currLanguage = currLanguage == Languages.Esp ? Languages.Eng : Languages.Esp;
        foreach(Translatable t in translatables) t.UpdateLanguage((int)currLanguage);
    }
}
