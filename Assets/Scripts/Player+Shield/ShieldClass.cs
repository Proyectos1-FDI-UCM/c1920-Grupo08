using UnityEngine;

// Script para almacenar los tipos de escudo y reconocerlo durante colisiones
public enum ShieldType 
{
    carDoor,
    woodenPlank,    
    sewerCap,   
    ballisticShield,
    roadSign
}

[System.Serializable]
public class Shield 
{
    public ShieldType shieldType;
    public float weight;
    public float durability;
    public Sprite sprite;
}

public class ShieldClass : MonoBehaviour 
{
    // ESTO ES EL ESCUDO
}