using UnityEngine;

public class ActiveShield : MonoBehaviour
{
    GameObject shield;
    bool shieldBroken;

    void Start()
    {      
        shield = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        shieldBroken = false;
        shield.SetActive(false);
    }

    void Update()
    {
        if (shieldBroken)
        {
            shield.SetActive(false);
        }

		else 
        {
            if (Input.GetButtonDown("Fire1") && !shield.activeSelf)
            {
                
                shield.SetActive(true);
            }
           
            else if (Input.GetButtonUp("Fire1") && shield.activeSelf)
            {
                
                shield.SetActive(false);
            }
        }        
    }

    public void ShieldBroken(bool state) 
    {
        shieldBroken = state;
    }
}
