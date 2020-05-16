using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cepo : MonoBehaviour
{

    [SerializeField] float retencion;
    [SerializeField] Sound sound;
    [SerializeField] Animator animator;
    PlayerController playerController;

    void OnTriggerEnter2D(Collider2D other)
    {
        playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.Stunned(retencion);
            animator.SetBool("Activated", true);
            AudioManager.instance.PlaySoundOnce(sound);
            Destroy(this.gameObject, retencion);
        }
    }
}
