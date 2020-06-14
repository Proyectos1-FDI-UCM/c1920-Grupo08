using UnityEngine;

//Este script desactiva el collider de su hijo cuando el jugador pulsa usar estando al lado
//Si se activa la cerradura, no se abrirá si el jugador no tiene una llave, en cual caso eliminará esa llave
[RequireComponent(typeof(BoxCollider2D))]
public class Doors : MonoBehaviour
{
    [SerializeField] Sound sound;
    [SerializeField]
    bool locked=false;
    [SerializeField]
    Sprite open;
    BoxCollider2D bc;
    SpriteRenderer sr;
    bool isOpen = false;
    private void Start()
    {
        bc = transform.GetChild(0).GetComponent<BoxCollider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            if (Input.GetButtonDown("Use")&&!isOpen)
            {
                if (!locked || GameManager.instance.HasKey())
                {
                    isOpen = true;
                    bc.enabled = false;
                    sr.sprite = open;
                    transform.GetChild(0).transform.localPosition = new Vector3(0.4f, 0f, 0f);
                    AudioManager.instance.PlaySoundOnce(sound);
                }

                else
                {                    
                    GameManager.instance.OnDialogue("Necesito una llave para abrir esta puerta");
                }                
            }            
        }
    }
}
