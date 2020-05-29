using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bubbleText;
    string dialogue;
    private float typeSpeed = 0.1f;
    AudioSource audioS;
    [SerializeField]GameObject a,b;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        dialogue = bubbleText.text;
        bubbleText.text = "";
        StartCoroutine(Type());
    }

    private IEnumerator Type()
    {
        yield return new WaitForSeconds(1f);
        audioS.Play();
        foreach (char c in dialogue.ToCharArray())
        {
            bubbleText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        audioS.Stop();
        yield return new WaitForSeconds(2f);
        a.SetActive(false);
        yield return new WaitForSeconds(2f);
        b.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneLoader.instance.NextScene();
    }    
}
