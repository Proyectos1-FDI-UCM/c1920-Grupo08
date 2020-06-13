using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	[SerializeField] BurstSpawner bs;
	[SerializeField] Sound sound;
	[SerializeField] GameObject explosionEffect;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
		{			
			GameManager.instance.OnHit(collision.gameObject, 300f);
		}

		Instantiate(explosionEffect, this.gameObject.transform.position, Quaternion.identity);
		AudioManager.instance.PlaySoundOnce(sound);
		bs.enabled = true;
	}
}
