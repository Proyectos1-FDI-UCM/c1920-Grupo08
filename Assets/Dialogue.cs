using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Text texto;

    // 
    public string frase;    

    private float speed = 0.08f;
    
    void Start()
    {
        texto.text = "";
        StartCoroutine(escribir());        
    }   

    private IEnumerator escribir()
    {
        foreach (char letter in frase.ToCharArray())
        {
            texto.text += letter;
            yield return new WaitForSeconds(speed);
        }

        yield return new WaitForSeconds(1f);

        this.gameObject.SetActive(false);
    }
}
