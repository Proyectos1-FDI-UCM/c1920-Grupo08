using UnityEngine;

// Se encarga de avisar de que el jugador a llegado al final del nivel.
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
