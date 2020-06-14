using UnityEngine;

// Script auxiliar para la animación del tronco
public class Rotate : MonoBehaviour
{
    enum Direction { Left, Right };
    [SerializeField]
    Direction direction;
    [SerializeField]
    float rotateSpeed;
    public void Update()
    {
        if (direction == Direction.Right)
            transform.Rotate(0f, 0f, -rotateSpeed);
        else
            transform.Rotate(0f, 0f, rotateSpeed);
    }

    public void RotateDirection(char d)
    {
        if (d == 'l')
        {
            direction = Direction.Left;
        }
        else if (d == 'r')
        {
            direction = Direction.Right;
        }
    }
}
