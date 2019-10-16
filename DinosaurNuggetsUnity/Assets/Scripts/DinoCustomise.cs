using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum HatType
{
    None,
    Cowboy,
    Crown,
    Helicopter,
    SmallRussian,
    LargeRussian,
    Shark,
    Snapback,
    SpaceHelmet,
    SmallTophat,
    LargeTophat,
    Chef,
    Party
}


public class DinoCustomise : MonoBehaviour
{
    [SerializeField] public HatType hatType;
}
