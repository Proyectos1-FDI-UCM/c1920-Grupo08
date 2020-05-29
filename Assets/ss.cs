using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ss : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bubbleText;
    string dialogue;
    private float typeSpeed = 0.1f;
    AudioSource audioS;    

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        dialogue = bubbleText.text;
        bubbleText.text = "";
        StartCoroutine(Type());
    }    

    private IEnumerator Type()
    {
        foreach (char c in dialogue.ToCharArray())
        {
            bubbleText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        audioS.Stop();        
    }
}
