using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageModifier : Translatable
{
    [SerializeField] protected TransSprite transSprite;
    private Image img;

    public override void UpdateLanguage(int l)
    {
        img.sprite = transSprite.Get(l);
    }

    public void Start()
    {
        base.Start();
        img = GetComponent<Image>();
        if (img == null)
        {
            Debug.LogError("ImageModifier attached to object with no Image component.");
        }
        img.sprite = transSprite.Get((int)LanguageManager.currLanguage);
    }
}
