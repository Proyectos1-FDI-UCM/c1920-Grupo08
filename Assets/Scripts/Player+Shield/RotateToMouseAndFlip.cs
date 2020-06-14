using UnityEngine;

// Se encarga de rotar el hombro en dirección al ratón y colocarlo de manera correcta.
public class RotateToMouseAndFlip : MonoBehaviour
{
    Vector2 aimV;
    Vector2 direction, mousePos, playerPos;
    Transform player;    

    private void Start()
    {
        player = gameObject.transform.parent.gameObject.transform;
    }
    void Update()
    {
        // Almacena la posición del ratón en un vector
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPos = player.position;

        if (true) 
        {
            if (mousePos.x > playerPos.x)
            {
                if (player.localScale.x < 0) player.localScale -= new Vector3(2 * player.localScale.x, 0, 0);
                direction = new Vector2(mousePos.x - playerPos.x, mousePos.y - playerPos.y);
                transform.right = direction;
            }
            else
            {
                if (player.localScale.x > 0) player.localScale -= new Vector3(2 * player.localScale.x, 0, 0);
                direction = new Vector2(playerPos.x - mousePos.x, playerPos.y - mousePos.y);
                transform.right = direction;
            }
        }
    }
}
