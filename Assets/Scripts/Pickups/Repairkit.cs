using UnityEngine;

// Repara el escudo al recogerlo
public class Repairkit : MonoBehaviour
{

    [SerializeField] private float reapairvalue = 20f;
    [SerializeField] private Sound sound;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            if (Input.GetButtonDown("Use"))
            {
                AudioManager.instance.PlaySoundOnce(sound);
                GameManager.instance.OnRepair(reapairvalue);
                GameManager.instance.OnDialogue("Espero que aguante...");
                Destroy(this.gameObject);
            }
        }
    }
}
