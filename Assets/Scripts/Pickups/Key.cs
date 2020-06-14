using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] Sound sound;

    //Este script notifica al GM cuando el jugador recoge una llave
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("good");
        if (Input.GetButtonDown("Use"))
        {
            AudioManager.instance.PlaySoundOnce(sound);
            GameManager.instance.KeyPickup();
            GameManager.instance.OnDialogue("Ahora a buscar la puerta que abre...");
            Destroy(this.gameObject);
        }
    }
}
