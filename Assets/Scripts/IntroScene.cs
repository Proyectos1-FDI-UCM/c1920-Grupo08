using System.Collections;
using UnityEngine;
using TMPro;

// Este script se encarga unicamente de la escena introductoria mediante una coroutine. 
// Escribe un texto dado y muestra el título del juego esperando los tiempos para crear un efecto de vídeo introductorio.
public class IntroScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bubbleText;
    [SerializeField] TextMeshProUGUI skipPrompt;
    [SerializeField] GameObject a, b;
    [SerializeField] TransString introText;
    [SerializeField] KeyCode skipKey;
    [SerializeField] private float skipPromptDuration = 1f;
    [SerializeField] private float requiredSkipTime = 1f; // Tiempo que hay que sujetar la tecla de saltar para que salte

    string dialogue;
    private float typeSpeed = 0.1f;
    AudioSource audioS;
    private float remainingSkipPromptTime = 0f;
    private float keyDownTimestamp = -1f;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        dialogue = introText.Get();
        bubbleText.text = "";
        StartCoroutine(Type());
        skipPrompt.enabled = false;
    }

    private void Update()
    {
        if(remainingSkipPromptTime > 0f)
            remainingSkipPromptTime -= Time.deltaTime;

        if (Input.anyKeyDown)
        {
            skipPrompt.enabled = true;
            remainingSkipPromptTime = skipPromptDuration;
        }
        else if (remainingSkipPromptTime < 0f)
            skipPrompt.enabled = false;
        
        if (Input.GetKeyDown(skipKey))
        {
            keyDownTimestamp = Time.time;
        }
        if (Input.GetKey(skipKey) && keyDownTimestamp > 0f && Time.time - keyDownTimestamp > requiredSkipTime)
        {
            SceneLoader.instance.NextScene();
            keyDownTimestamp = -1f;
        }
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
