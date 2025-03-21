﻿using System.Collections;
using UnityEditor;
using UnityEngine;

// Este script es el encargado de activar el explosivo de la mina según su comportamiento
public class SimpleMine : MonoBehaviour
{
	[SerializeField] bool debug;
	
	[SerializeField] float damage;
	[SerializeField] GameObject explosive;
	[SerializeField] float explosionRadius;
	[SerializeField] float delay = 0f;
	[SerializeField] ParticleSystem detectionPulse;

	void Awake()
	{
		if (delay > 0f)
			transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
		explosive.GetComponent<SimpleExplosive>().radius = explosionRadius;
		explosive.GetComponent<SimpleExplosive>().damage = damage;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
		{
			if(detectionPulse != null) { detectionPulse.Emit(1); }
			StartCoroutine(Ignite());
		}
	}

	IEnumerator Ignite()
	{
		if (debug) Debug.Log("Mina activada");
		yield return new WaitForSeconds(delay);
		explosive.SetActive(true);
	}
}
