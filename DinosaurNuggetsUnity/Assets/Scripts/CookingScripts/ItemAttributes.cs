using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAttributes : MonoBehaviour
{
    [SerializeField] public IngredientType GameType;
    [SerializeField] public bool Burnable = false;
    
    [SerializeField] public bool Locked = false;
    [SerializeField] public float unlockTime = 3.0f;
    [SerializeField] public Image unlockImage = null;

    [HideInInspector] public GameObject LoadingUI;
    [HideInInspector] public Transform initialParent;
    [HideInInspector] public bool currentlyBurning = false;
    [HideInInspector] public bool wasLocked = false;
    [HideInInspector] public bool beingUsed = false;
    [HideInInspector] public bool onStove = false;
    [HideInInspector] public bool onCuttingBoard = false;
    [HideInInspector] public Transform currentCuttingBoard = null;
    [HideInInspector] public bool initPosition;
    [HideInInspector] public Rigidbody rbd;

    private void Start()
    {
        initialParent = gameObject.transform.parent;
        rbd = GetComponent<Rigidbody>();
        rbd.constraints = RigidbodyConstraints.FreezeAll;
        wasLocked = Locked;
        if(Locked)
            LoadingUI = transform.GetChild(0).gameObject;
    }

}
