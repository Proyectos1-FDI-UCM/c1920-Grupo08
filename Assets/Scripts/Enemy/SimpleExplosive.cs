using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SimpleExplosive : MonoBehaviour
{
	[SerializeField] float damage, force;
	[SerializeField] Sound sound;
	[SerializeField] GameObject explosionEffect;
	Vector2 direction;
	
	private void Start()
	{
		Invoke("Des", 0.1f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
		{
			GameManager.instance.OnHit(collision.gameObject, damage);
			direction = collision.gameObject.transform.position-transform.position;
			direction.Normalize();
			collision.GetComponent<Rigidbody2D>().AddForce(direction * force,ForceMode2D.Impulse);
		}
	}

	private void Des()
	{
		Instantiate(explosionEffect, this.gameObject.transform.position, Quaternion.identity);
		AudioManager.instance.PlaySoundOnce(sound);
		Destroy(this.gameObject.transform.parent.gameObject);
	}
}
