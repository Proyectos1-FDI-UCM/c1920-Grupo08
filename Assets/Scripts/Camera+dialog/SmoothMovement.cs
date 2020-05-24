using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta componente mueve un objeto hasta un transform y a una rapidez determinados
//Esta componente está ideada para la cámara y no debe usarse para ningún objeto con RigidBody
public class SmoothMovement : MonoBehaviour
{/*
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
        //Hacemos que sea hija del jugador para que le siga
        transform.SetParent(dest);
    }
    //Esta versión del método se usa cuando el objetivo solo es un cambio de la posición relativa al 
    //padre del objeto en movimiento
    public void MoveTo(float offset, float spd)
    {
        speed = spd;
        dest = null;
        aux = offset;
        active = true;
    }*/






    //La velocidad de la transición
    float speed;
    //El transform destino. No puede ser un simple vector3 porque puede ser un objeto en movimiento,
    //conque el destino puede variar durante la trayectoria
    Vector3 dest;
    //Controla si seguimos o no al jugador
    bool followPlayer = true;
    //La posición del hijo CameraPosition del jugador
    [SerializeField]
    GameObject playerPos;
    //La velocidad de la cámara cuando sigue al jugador
    [SerializeField]
    float playerFollowSpeed = 100;
    //Distancia al objetivo
    float distance;
    //El número de zonas cinemáticas en las que estamos
    int inside = 0;
    private void Start()
    {
        //Debug.Log("before start: " + followPlayer);
        followPlayer = true;
        //Debug.Log("after start: " + followPlayer);

        speed = playerFollowSpeed;
    }
    private void Update()
    {
        //Debug.Log("Each frame: " + followPlayer);
        //Debug.Log(followPlayer);
        if (followPlayer)
        {
            dest = playerPos.transform.position;
        }
        distance = Vector3.Distance(transform.position, dest);
        //Si no estamos en el objetivo, movemos (se usa < 0.01 en vez de == porque son floats)
        if (!(distance < 0.01f))
        {
            //Movemos hacia el destino. El parámetro float limita cuánto se puede mover por frame
            transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime * distance / 4);
        }
    }

    //Este método sirve para ser llamado desde otras componentes e inicializar los datos de un desplazamiento nuevo
    public void MoveToAnchor(Vector3 destination, float spd)
    {
        //Debug.Log("MoveToAnchor");
        speed = spd;
        dest = destination;
        Debug.Log("before change: "+followPlayer);
        followPlayer = false;
        Debug.Log("after change: "+followPlayer);
    }
    
    public void ReturnToPlayer()
    {
        speed = playerFollowSpeed;
        //followPlayer = true;
    }

    public void EnterZone()
    {
        inside++;
    }

    public void ExitZone()
    {
        inside--;
    }

    public int InsideZones()
    {
        return inside;
    }
}
