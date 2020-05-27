using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCamOnDeath : MonoBehaviour
{
    private void OnDestroy()
    {
        Destroy(SmoothMovement.mainCamera);
    }
}
