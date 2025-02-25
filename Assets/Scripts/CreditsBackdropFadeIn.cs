using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsBackdropFadeIn : MonoBehaviour
{
    // Fades in a sprite up to a certain opacity value midValue when the player crosses the x position of startFade.
    // Fully fades in the sprite after the player crosses endFade.
    Transform playerPos;
    [SerializeField] Transform startFade, endFade;
    [SerializeField] float midValue, startDuration, endDuration;
    [SerializeField] SpriteRenderer sprite;

    bool started = false, ended = false;

    void Start()
    {
        playerPos = GameManager.instance.player.transform;
    }

    void Update()
    {
        if (!started && playerPos.position.x > startFade.position.x)
        {
            StartCoroutine(Fade(midValue, startDuration));
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
        float startOpacity = sprite.color.a;
        float startTime = Time.time;

        while (sprite.color.a < finalOpacity)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, (startOpacity + (Time.time - startTime) * (finalOpacity - startOpacity) / duration));
            yield return null;
        }
    }
}
