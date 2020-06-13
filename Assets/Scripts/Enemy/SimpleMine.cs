using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMine : MonoBehaviour
{
	[SerializeField] GameObject explosive;
	[SerializeField] float delay = 0f;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
		{
			StartCoroutine(Ignite());
		}
	}

	IEnumerator Ignite()
	{
		Debug.Log("mina activada");
		yield return new WaitForSeconds(delay);
		explosive.SetActive(true);
	}
}
