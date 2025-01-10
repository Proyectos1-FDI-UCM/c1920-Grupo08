using System.Collections;
using UnityEngine;

// Este script es el encargado de activar el explosivo de la mina según su comportamiento
public class SimpleMine : MonoBehaviour
{
	[SerializeField] bool debug;
	
	[SerializeField] GameObject explosive;
	[SerializeField] float delay = 0f;
	[SerializeField] ParticleSystem detectionPulse;

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
