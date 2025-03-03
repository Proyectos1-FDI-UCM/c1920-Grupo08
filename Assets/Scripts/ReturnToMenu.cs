using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMenu : MonoBehaviour
{
    public void _ReturnToMenu()
    {
        SceneLoader.instance.LoadMainMenu();
    }
}
