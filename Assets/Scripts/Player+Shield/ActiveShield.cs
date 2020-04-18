using UnityEngine;

public class ActiveShield : MonoBehaviour
{
    //FIX TEMPORAL, NO TERMINADO
    // Falta comprobar si hay un escudo, la vida de este etc...

    GameObject hand;    

    void Start()
    {
        hand = gameObject.transform.GetChild(0).GetChild(0).gameObject;        
        hand.SetActive(false);
    }

    void Update()
    {        
            // ... se pulsa el ratón y el escudo no está activado
            if (Input.GetButtonDown("Fire1") && !hand.activeSelf)
            {
                // ... se activa el escudo
                hand.SetActive(true);
            }
            // ... y se suelta el ratón y el escudo está activado
            else if (Input.GetButtonUp("Fire1") && hand.activeSelf)
            { 
                // ... el escudo se desactiva
                hand.SetActive(false);
            }        
    }
}
