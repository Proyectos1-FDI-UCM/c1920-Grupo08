using UnityEngine;

// Este script se usa en la bomba se encarga de activar el spawner de metralla y matar al jugador en caso de colisionar con el
public class Bomb : MonoBehaviour
{
	[SerializeField] bool debug;

	[SerializeField] BurstSpawner bs;
	[SerializeField] Sound sound;
	[SerializeField] GameObject explosionEffect;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
		{			
			GameManager.instance.OnHit(collision.gameObject, 300f);
			if (debug) Debug.Log("Bomb collision detected");
		}

		Instantiate(explosionEffect, this.gameObject.transform.position, Quaternion.identity);
		AudioManager.instance.PlaySoundOnce(sound);
		bs.enabled = true;
		Destroy(this.gameObject);
	}
}
