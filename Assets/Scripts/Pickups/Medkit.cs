using UnityEngine;

public class Medkit : MonoBehaviour
{
    
    [SerializeField] private float heal = 100f;
    [SerializeField] private Sound sound; 

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            if (Input.GetButtonDown("Use"))
            {
                AudioManager.instance.PlaySoundOnce(sound);
                GameManager.instance.OnHeal(heal);
                GameManager.instance.OnDialogue("Ahora me siento mucho mejor");
                Destroy(this.gameObject);
            }
        }
    }
}
