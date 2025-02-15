using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Translatable : MonoBehaviour
{
    public void Start()
    {
        if (GameManager.instance.languageManager == null) Debug.Log("Language manager null");
        GameManager.instance.languageManager.AddTranslatable(this);
    }

    public void OnDestroy()
    {
        GameManager.instance.languageManager.RemoveTranslatable(this);
    }

    public abstract void UpdateLanguage(int l);
}
