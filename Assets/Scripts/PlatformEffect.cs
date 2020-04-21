using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlatformEffect : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float moveY;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveY = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Crouch") && (moveY < 0)) 
        {
            effector.rotationalOffset = 180f;
            Invoke("ResetRotation", 0.5f);
        }
    }

    void ResetRotation() 
    {
        effector.rotationalOffset = 0f;
    }
}
