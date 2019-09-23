using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Types
{
    None,
    Pot,
    Pan,
    CuttingBoard
}
public class BeingUsed : MonoBehaviour
{
    public bool beingUsed = false;
    public bool onStove = false;
    [SerializeField] Types type;
}
