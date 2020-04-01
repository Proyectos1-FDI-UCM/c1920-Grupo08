using UnityEngine;

public class ActiveShield : MonoBehaviour
{
    GameObject hand;
    ObjectHealth shield;

    void Start()
    {
        hand = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        shield = hand.transform.GetChild(0).GetComponent<ObjectHealth>();
        hand.SetActive(false);
    }

    void Update()
    {
        // Si el escudo tiene vida...
        if(shield.health > 0) 
        {
            // ... se pulsa el ratón y el escudo no está sacado...
            if (Input.GetButtonDown("Fire1") && !hand.active)
            {
                // ... se activa el escudo
                hand.SetActive(true);
            }
            // ... y se suelta el ratón...
            else if (Input.GetButtonUp("Fire1"))
            { 
                // ... el escudo se desactiva
                hand.SetActive(false);
            }
        }
        // Si el escudo no tiene vida y esta activo...
        else if(hand.active)
        {
            // ... se desactiva
            hand.SetActive(false);
        }
    }
}
