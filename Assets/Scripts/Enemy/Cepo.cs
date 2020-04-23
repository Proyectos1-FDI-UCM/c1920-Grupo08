using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cepo : MonoBehaviour
{

    [SerializeField] float retencion;
    [SerializeField] Sound sound;
    PlayerMove moving;

    void OnTriggerEnter2D(Collider2D other)
    {
        moving = other.GetComponent<PlayerMove>();
        if (moving != null)
        {
            moving.Stunned(retencion);
            AudioManager.instance.PlaySoundOnce(sound);
            Destroy(this.gameObject, retencion);
        }
    }
}
