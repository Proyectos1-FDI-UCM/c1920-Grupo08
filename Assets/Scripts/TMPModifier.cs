using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class TMPModifier : Translatable
{
    [SerializeField] protected TransString transString;
    private TextMeshProUGUI tmp;

    public override void UpdateLanguage(int l)
    {
        tmp.text = transString.Get(l);
    }

    public void Start()
    {
        base.Start();
        tmp = GetComponent<TextMeshProUGUI>();
        if (tmp == null) 
        {
            Debug.LogError("StringModifier attached to object with no TextMeshProUGUI component.");
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(gameObject);
#endif
        }
        if (transString == null)
        {
            Debug.LogError("TMPModifier " + transform.name + " has no assigned TransString.");
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(gameObject);
#endif
        }
        tmp.text = transString.Get((int)LanguageManager.currLanguage);
    }
}
