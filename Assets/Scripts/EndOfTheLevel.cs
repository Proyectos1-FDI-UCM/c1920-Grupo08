using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfTheLevel : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null) 
		{
			SceneLoader.instance.NextScene();
		}
	}
}
