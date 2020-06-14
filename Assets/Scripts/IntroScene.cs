using System.Collections;
using UnityEngine;
using TMPro;

// Este script se encarga unicamente de la escena introductoria mediante una coroutine. 
// Escribe un texto dado y muestra el título del juego esperando los tiempos para crear un effecto de vídeo introductorio.
public class IntroScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bubbleText;
    [SerializeField] GameObject a, b;

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
