using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    //El tamaño objetivo
    float size;
    //La componente cámara
    Camera camera;
    //Una variable de control para no ejecutar siempre el contenido del Update()
    bool active = false;

    void Start()
    {
        //Guardamos una referencia a la componente Camera de la cámara para poder ajustar luego
        //el tamaño
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (active)
        {
            //Hacemos una progresión logarítmica (?) del tamaño de la cámara hasta que sea del tamaño requerido
            camera.orthographicSize += (size - camera.orthographicSize) / 3;
            //Si estamos en el tamaño objetivo, paramos el movimiento (se usa < 0.01 en vez de == porque son floats)
            if (Mathf.Abs(camera.orthographicSize - size) < 0.01f)
                active = false;
        }
    }

    //Este método sirve para ser llamado desde otras componentes e inicializar los datos de un cambio de tamaño nuevo
    public void ChangeSize(float newSize)
    {
        size = newSize;
        active = true;
    }

    public float GetSize()
    {
        Debug.Log("camsize "+camera.orthographicSize);
        return camera.orthographicSize;
    }
}
