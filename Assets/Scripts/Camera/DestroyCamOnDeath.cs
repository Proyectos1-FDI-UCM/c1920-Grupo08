using UnityEngine;

// Auxiliar para el correcto funcionamiento de la cámara
public class DestroyCamOnDeath : MonoBehaviour
{
    private void OnDestroy()
    {
        Destroy(SmoothMovement.mainCamera);
    }
}
