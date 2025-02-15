using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TransString", menuName = "ScriptableObjects/TranslatableString", order = 1)]
public class TransString : ScriptableObject
{
    [SerializeField] string[] translations = Enum.GetNames(typeof(LanguageManager.Languages));

    public string Get(int l)
    {
        return translations[l];
    }

    public string Get()
    {
        return translations[(int)LanguageManager.currLanguage];
    }
}