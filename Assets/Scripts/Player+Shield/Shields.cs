using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShieldType 
{
    carDoor,
    woodenPlank,
    largePicture,
    sewerCap,
    ancientShield,
    ritoShield,
    ballisticShield
}

[System.Serializable]
public class Shield 
{
    public ShieldType shieldType;
    public float weight;
    public float durability;
    public Sprite sprite;
}