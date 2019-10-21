using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingUsed : MonoBehaviour
{
    [SerializeField] public IngredientType GameType;
    [SerializeField] public bool Burnable = false;
    [HideInInspector] public Transform initialParent;
    [HideInInspector] public bool beingUsed = false;
    [HideInInspector] public bool onStove = false;
    [HideInInspector] public bool Locked = false;
    [HideInInspector] public bool initPosition;
    [HideInInspector] public Rigidbody rbd;

    private void Start()
    {
        initialParent = gameObject.transform.parent;
        rbd = GetComponent<Rigidbody>();
        rbd.constraints = RigidbodyConstraints.FreezeAll;
    }
}
