using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsBackdropFadeIn : MonoBehaviour
{
    // Fades in a sprite up to a certain opacity value midValue when the player crosses the x position of startFade.
    // Fully fades in the sprite after the player crosses endFade.
    Transform playerPos;
    [SerializeField] Transform startFade, endFade;
    [SerializeField] float midValue, startDuration, endDuration;
    [SerializeField] Image img;
    [SerializeField] UIManager uiManager;
    HoldToSkip holdToSkip;

    bool started = false, ended = false;

    void Start()
    {
        playerPos = GameManager.instance.player.transform;
        holdToSkip = GetComponent<HoldToSkip>();
    }

    void Update()
    {
        if (!started && playerPos.position.x > startFade.position.x)
        {
            StartCoroutine(Fade(midValue, startDuration));
            holdToSkip.enabled = true;
            uiManager.enabled = false;
            started = true;
        }

        if (!ended && playerPos.position.x > endFade.position.x)
        {
            StartCoroutine(Fade(1f, endDuration));
            ended = true;
        }
    }

    // Gradually increases the sprite's opacity up to finalOpacity over duration seconds
    IEnumerator Fade(float finalOpacity, float duration)
    {
        float startOpacity = img.color.a;
        float startTime = Time.time;

        while (img.color.a < finalOpacity)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, (startOpacity + (Time.time - startTime) * (finalOpacity - startOpacity) / duration));
            yield return null;
        }
    }
}
