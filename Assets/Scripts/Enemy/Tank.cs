﻿using System.Collections;
using UnityEngine;

// El tanque dispara una bala a un punto fijo de manera continua siempre que el jugador este en rango

[RequireComponent(typeof(CircleCollider2D))]
public class Tank : MonoBehaviour
{
    [SerializeField] GameObject projectile,muzzleFlash;
    [SerializeField] bool debug;
    [SerializeField] Transform firePoint;
    [SerializeField] float shotCD;
    [SerializeField] Sound sound;
    float elapsedTime = 0f;
    bool targetInside;
   
    void Start()
    {
        muzzleFlash.SetActive(false);
        targetInside = false;
    }

    private void Update()
    {
        if (targetInside) 
        {
            if (Time.time > elapsedTime)
            {
                AudioManager.instance.PlaySoundOnce(sound);
                StartCoroutine(MuzzleFlash());
                Instantiate(projectile, firePoint.position, firePoint.rotation);

                // Aumenta el contador de disparo
                elapsedTime = Time.time + shotCD;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            if (debug) Debug.Log("PlayerENTER");
            elapsedTime = Time.time;
            targetInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null) 
        {
            if (debug) Debug.Log("PlayerIN");
            targetInside = false;
        }
    }

    IEnumerator MuzzleFlash() 
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        muzzleFlash.SetActive(false);
    }
}
