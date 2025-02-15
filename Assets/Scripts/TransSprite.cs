using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TransSprite", menuName = "ScriptableObjects/TranslatableSprite", order = 1)]
public class TransSprite : ScriptableObject
{
    [SerializeField] Sprite[] sprites = new Sprite[(Enum.GetNames(typeof(LanguageManager.Languages))).Length];

    public Sprite Get(int l)
    {
        return sprites[l];
    }

    public Sprite Get()
    {
        return sprites[(int)LanguageManager.currLanguage];
    }
}