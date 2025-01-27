using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "TransString", menuName = "ScriptableObjects/TranslatableString", order = 1)]
public class TransString : ScriptableObject
{
    [SerializeField] string[] translations  = Enum.GetNames(typeof(GameManager.Languages));

    public string Get()
    {
        return translations[(int)(GameManager.instance.currLanguage)];
    }
}