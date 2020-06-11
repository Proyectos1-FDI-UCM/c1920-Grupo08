using UnityEngine;

public class ShieldController : MonoBehaviour
{
    GameObject shield;
    bool isBroken;

    void Start()
    {      
        shield = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        isBroken = false;
        shield.SetActive(false);
    }

    void Update()
    {
        if (isBroken)
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
        isBroken = state;
    }
}
