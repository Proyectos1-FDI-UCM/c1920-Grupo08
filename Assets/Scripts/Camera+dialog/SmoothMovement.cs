/*
 * using System.Collections;
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
    Vector3 obj;
    //Una variable de control para no ejecutar siempre el contenido del Update()
    bool active = false;
    private void Update()
    {
        if (active)
        {
            //Si el destinatario es un transform
            if (dest != null)
                obj = dest.position;
            //Movemos hacia el destino. El parámetro float limita cuánto se puede mover por frame
            transform.position = Vector3.MoveTowards(transform.position, obj, speed * Time.deltaTime);

            //Si estamos en el objetivo, paramos el movimiento (se usa < 0.01 en vez de == porque son floats)
            if (Vector3.Distance(transform.position, obj) < 0.01f)
            {
                if (dest != null)
                    //Hacemos que sea hija del objeto destino
                    transform.SetParent(dest);
                //Desactivamos active para dar por acabado el movimiento
                active = false;
            }
        }
    }

    //Este método sirve para ser llamado desde otras componentes e inicializar los datos de un desplazamiento nuevo
    public void MoveTo(Transform destination, float spd)
    {
        speed = spd;
        dest = destination;
        active = true;
    }

    //Eta versión del método se usa cuando el objetivo solo es un cambio de la posición relativa al 
    //padre del objeto en movimiento
    public void MoveTo(Vector3 offset, float spd)
    {
        speed = spd;
        dest = null;
        obj = offset;
        active = true;
    }
}*/
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
    float aux;
    //Una variable de control para no ejecutar siempre el contenido del Update()
    bool active = false;
    private void Update()
    {
        if (active && dest!=null)
        {
            //Movemos hacia el destino. El parámetro float limita cuánto se puede mover por frame
            transform.position = Vector3.MoveTowards(transform.position, dest.position, speed*Time.deltaTime);
            //Si estamos en el objetivo, paramos el movimiento (se usa < 0.01 en vez de == porque son floats)
            if (Vector3.Distance(transform.position, dest.position) < 0.01f)
            {
                //Hacemos que sea hija del jugador para que le siga
                transform.SetParent(dest);
                //Desactivamos active para dar por acabado el movimiento
                active = false;
            }
        }
        else if(active)
        {
            transform.Translate(0, 0.01f*speed, 0);
            aux -= 0.01f*speed;
            if (aux < 0f)
            {
                active = false;
            }
        }
    }

    //Este método sirve para ser llamado desde otras componentes e inicializar los datos de un desplazamiento nuevo
    public void MoveTo(Transform destination, float spd)
    {
        speed = spd;
        dest = destination;
        active = true;
    }
    //Esta versión del método se usa cuando el objetivo solo es un cambio de la posición relativa al 
    //padre del objeto en movimiento
    public void MoveTo(float offset, float spd)
    {
        speed = spd;
        dest = null;
        aux = offset;
        active = true;
    }
}
