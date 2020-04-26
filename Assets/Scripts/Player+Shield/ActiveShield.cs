using UnityEngine;

public class ActiveShield : MonoBehaviour
{
    GameObject shield;    

    void Start()
    {      
        shield = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        shield.SetActive(false);
    }

    void Update()
    {
        if (GameManager.instance.ShieldBroken())
        {
            shield.SetActive(false);
        }

        // ... se pulsa el ratón y el escudo no está activado
        else if (Input.GetButtonDown("Fire1") && !shield.activeSelf && !GameManager.instance.ShieldBroken())
        {
            // ... se activa el escudo
            shield.SetActive(true);            
        }
        // ... y se suelta el ratón y el escudo está activado
        else if (Input.GetButtonUp("Fire1") && shield.activeSelf)
        {
            // ... el escudo se desactiva
            shield.SetActive(false);
        }
    }
}
