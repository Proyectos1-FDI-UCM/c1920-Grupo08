using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxValue(int value) 
    {
        // Longitud del slider
        slider.maxValue = value;
        // Coloca el slider a
        slider.value = value;
    }
    public void SetValue(int value) 
    {
        slider.value = value;
    }
}
