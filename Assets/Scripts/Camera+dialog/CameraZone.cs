using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Esta componente detecta cuando el CameraPosition entra en la zona de influencia del objeto asociado. El 
 objeto necesitará un collider habilitado como trigger (no importa el tipo de collider). 
 Al entrar el jugador en esta zona, la cámara se desplaza hasta un punto predefinido por la posición del hijo
 de este objeto, designado CameraAnchor. Además, cambiará el tamaño de la cámara a una predefinida para esta
 zona en particular.
 Al salir de la zona, los cambios se revierten.*/

public class CameraZone : MonoBehaviour
{
    //El tamaño que adquirirá la cámara cuando esté en esta zona
    [SerializeField]
    float CameraSize = 5f;
    //La velocidad a la que la cámara se moverá hasta y desde esta zona
    [SerializeField]
    float snapSpeed = 1f;
    [SerializeField]
    bool UseAnchor = true;
    //El desplazamiento vertical de la cámara (solo usado en zonas sin ancla)
    [SerializeField]
    float offset=0f;
    //La cámara en sí (no necesitamos guardar el objeto entero)
    Transform cam;
    //La posición donde se colocará la cámara en esta zona
    Transform anchor;
    //La posición relativa al jugador y el tamaño a los que habrá que devolver la cámara 
    //una vez el jugador sale de la zona
    Transform camerapos;
    float defaultSize;
    //Los scripts que tendremos que acceder de la cámara
    SmoothMovement movement;
    CameraSize camSize;
    
    void Start()
    {
        //Guardamos la posición de anclaje de la cámara
        anchor = transform.GetChild(0).GetComponent<Transform>();
        cam = null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Para ahorrarnos referencias públicas entre objetos, podemos hacer que la primera vez que 
        //se detecte la colisión de la cámara el script se guarde los datos de esta.
        if (cam == null)
        {
            //Este es el hijo CameraPosition del player.
            //Hacemos esta separación para que se pueda ajustar la posición de la cámara en relación 
            //al jugador.
            camerapos = collision.gameObject.transform;
            //Guardamos una referencia a la cámara
            cam = camerapos.transform.GetChild(0).transform;
            //Guardamos referencias a los scripts necesarios de la cámara
            movement = cam.gameObject.GetComponent<SmoothMovement>();
            camSize = cam.gameObject.GetComponent<CameraSize>();
            //Guardamos el tamaño base de la cámara
            defaultSize = camSize.GetSize();
        }

        if (UseAnchor)
        {
            //Movemos la cámara hasta el ancla
            movement.MoveTo(anchor, snapSpeed);
            //Hacemos que no sea hija de nadie
            cam.transform.parent = null;
        }
        else if (offset != 0)
        {
            //Si no queremos usar el ancla pero sí elevar la cámara, invocamos el método 
            //correspondiente en SmoothMovement
            
            movement.MoveTo(offset, snapSpeed);
        }
        //Cambiamos el tamaño de la cámara a la necesaria para esta zona
        camSize.ChangeSize(CameraSize);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //No es necesario asignar cam: para poder salir del trigger tiene que haber primero entrado

        //Movemos la cámara a su posición original relativa al jugador
        movement.MoveTo(camerapos, snapSpeed);
        //Devolvemos la cámara a su tamaño original
        camSize.ChangeSize(defaultSize);
    }
}
