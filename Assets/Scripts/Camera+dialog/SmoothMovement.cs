using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta componente mueve un objeto hasta un transform y a una rapidez determinados
//Esta componente está ideada para la cámara y no debe usarse para ningún objeto con RigidBody
public class SmoothMovement : MonoBehaviour
{
    //La velocidad de la transición
    float speed;
    //El transform destino. No puede ser un simple vector3 porque puede ser un objeto en movimiento,
    //conque el destino puede variar durante la trayectoria
    Transform dest;
    //Una variable de control para no ejecutar siempre el contenido del Update()
    bool active = false;
    private void Update()
    {
        if (active)
        {
            //Movemos hacia el destino. El parámetro float limita cuánto se puede mover por frame
            transform.position = Vector3.MoveTowards(transform.position, dest.position, speed*Time.deltaTime);
            //Si estamos en el objetivo, paramos el movimiento (se usa < 0.01 en vez de == porque son floats)
            if (Vector3.Distance(transform.position, dest.position) < 0.01f)
                active = false;
        }
    }

    //Este método sirve para ser llamado desde otras componentes e inicializar los datos de un desplazamiento nuevo
    public void MoveTo(Transform destination, float spd)
    {
        speed = spd;
        dest = destination;
        active = true;
    }
}
