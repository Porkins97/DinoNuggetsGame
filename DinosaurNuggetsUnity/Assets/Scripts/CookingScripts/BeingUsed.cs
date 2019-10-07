using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingUsed : MonoBehaviour
{
    [SerializeField] public IngredientType GameType;
    public Transform initialParent;
    public bool beingUsed = false;
    public bool onStove = false;
    public bool Burnable = false;
    public bool Locked = false;
    public bool initPosition;
    public Rigidbody rbd;

    private void Start()
    {
        initialParent = gameObject.transform.parent;
        rbd = GetComponent<Rigidbody>();
        rbd.constraints = RigidbodyConstraints.FreezeAll;
    }
}
