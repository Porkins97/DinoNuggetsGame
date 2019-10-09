using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum HatType
{
    None = 0,
    Cowboy = 1,
    Crown = 2,
    Helicopter = 3,
    RussianSmall = 4, 
    RussianLarge = 5,
    Shark = 6,
    Snapback = 7,
    SpaceHelmet = 8,
    TophatSmall = 9,
    TophatLarge = 10,
    Chefs = 11,
    Party = 12
}


public class DinoCustomise : MonoBehaviour
{
    [SerializeField] public HatType hatType;
}
