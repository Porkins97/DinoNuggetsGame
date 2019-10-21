using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum HatType
{
    Cowboy,
    Crown,
    Chef,
    Party,
    Helicopter,
    SmallRussian,
    LargeRussian,
    Shark,
    Snapback,
    SpaceHelmet,
    SmallTophat,
    LargeTophat,
    None
}


public class DinoCustomise : MonoBehaviour
{
    [SerializeField] public HatType hatType;
}
