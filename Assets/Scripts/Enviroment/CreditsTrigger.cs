using UnityEngine;

// Un trigger sencillo para los créditos del nivel final
public class CreditsTrigger : MonoBehaviour
{
    [SerializeField] GameObject credits;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null) 
        {
            credits.SetActive(true);
        }
    }
}
