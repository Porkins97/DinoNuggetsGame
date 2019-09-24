using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingUsed : MonoBehaviour
{
    [SerializeField] public IngredientType GameType;
    [HideInInspector] public Transform initialParent;
    public bool beingUsed = false;
    public bool onStove = false;
    public bool Burnable = false;
    public bool Locked = false;

    private void Start()
    {
        initialParent = gameObject.transform.parent;
    }
}
