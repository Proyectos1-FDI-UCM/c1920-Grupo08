using System.Collections;
using UnityEngine;
using TMPro;

// Este script se encarga unicamente de la escena introductoria mediante una coroutine. 
// Escribe un texto dado y muestra el título del juego esperando los tiempos para crear un efecto de vídeo introductorio.
public class IntroScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bubbleText;
    
    [SerializeField] GameObject a, b;
    [SerializeField] TransString introText;
    

    string dialogue;
    private float typeSpeed = 0.1f;
    AudioSource audioS;
    

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        dialogue = introText.Get();
        bubbleText.text = "";
        StartCoroutine(Type());
    }

    private IEnumerator Type()
    {
        yield return new WaitForSeconds(1f);

        audioS.Play();
        int i = 0;
        while (i < dialogue.Length)
        {
            // Hay que imprimir los saltos de línea todo de una, si no se ven los caracteres individuales del <BR>
            if (i < dialogue.Length - 4 && dialogue.Substring(i, 4) == "<BR>")
            {
                bubbleText.text += "<BR>";
                i += 4;
            }
            else
            {
                bubbleText.text += dialogue[i];
                ++i;
            }
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
