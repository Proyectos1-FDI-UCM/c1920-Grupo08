using UnityEngine;

public class Medkit : MonoBehaviour
{
    
    [SerializeField] private float heal = 100f;
    [SerializeField] private Sound sound; 
    [SerializeField] private TransString onUseMessage;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            if (Input.GetButtonDown("Use"))
            {
                AudioManager.instance.PlaySoundOnce(sound);
                GameManager.instance.OnHeal(heal);
                GameManager.instance.OnDialogue(onUseMessage.Get());
                Destroy(this.gameObject);
            }
        }
    }
}
