using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoldToSkip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI skipPrompt; 
    [SerializeField] KeyCode skipKey;
    [SerializeField] private float skipPromptDuration = 1f;
    [SerializeField] private float requiredSkipTime = 1f; // Tiempo que hay que sujetar la tecla de saltar para que salte
    private float remainingSkipPromptTime = 0f;
    private float keyDownTimestamp = -1f;

    void Awake()
    {
        skipPrompt.gameObject.SetActive(false);
    }

    void Update()
    {
        if (remainingSkipPromptTime > 0f)
            remainingSkipPromptTime -= Time.deltaTime;

        if (Input.anyKeyDown)
        {
            skipPrompt.gameObject.SetActive(true);
            remainingSkipPromptTime = skipPromptDuration;
        }
        else if (remainingSkipPromptTime < 0f)
            skipPrompt.gameObject.SetActive(false);

        if (Input.GetKeyDown(skipKey))
        {
            keyDownTimestamp = Time.time;
        }
        if (Input.GetKey(skipKey) && keyDownTimestamp > 0f && Time.time - keyDownTimestamp > requiredSkipTime)
        {
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
                SceneLoader.instance.NextScene();
            else
                SceneLoader.instance.LoadMainMenu();
            keyDownTimestamp = -1f;
        }
    }
}
