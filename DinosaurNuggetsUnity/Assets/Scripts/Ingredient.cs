﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    //if it is in the hand, Held = true
    public static bool GlobalHeldLeft = false;
    public static bool GlobalHeldRight = false;
    public static bool OvenInUse = false;
    private bool ThisItemIsBeingCarried = false;
    private bool Dead = false;
    private int HeldLeft = 0;
    private int HeldRight = 0;
    private int WhichHand = 0;
    public float TargetScale = 0.001f;
    public float ShrinkSpeed = 100f;

    private Rigidbody ThisRigidBody = null;
    public GameObject HandLeft;
    public GameObject HandRight;
    public GameObject Name;
    public GameObject Character;
    Collider ObjectCollider;
    public GameObject Burner;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Interactable";
        ThisRigidBody = GetComponent<Rigidbody>();
        //Fetch the GameObject's Collider (make sure they have a Collider component)
        ObjectCollider = GetComponent<Collider>();
        //Here the GameObject's Collider is not a trigger
        ObjectCollider.isTrigger = false;
        Burner = GameObject.Find("Oven_001/Burner");
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && ThisItemIsBeingCarried == true)
        {
            Drop();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            HeldLeft++;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            HeldRight++;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            HeldLeft--;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            HeldRight--;
        }

        if ((HeldLeft == 2) && GlobalHeldLeft == false)
        {
            GlobalHeldLeft = true;
            ThisItemIsBeingCarried = true;
            PickupLeft();
            WhichHand = 1;
        }
        if ((HeldRight == 2) && GlobalHeldRight == false)
        {
            GlobalHeldRight = true;
            ThisItemIsBeingCarried = true;
            PickupRight();
            WhichHand = 2;
        }

        if (Dead == true)
        {
            Name.transform.localScale = new Vector3(0.15f, 0.7f, 0.15f);
            Name.transform.localScale = Vector3.Lerp(Name.transform.localScale, new Vector3(TargetScale, TargetScale, TargetScale), Time.deltaTime * ShrinkSpeed);
            ObjectCollider.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character_Model_01")
        {
            HeldLeft++;
            HeldRight++;
        }

        if((collision.gameObject.tag == "Oven") && (OvenUtensil.OvenInUse == true))
        {
            ThisItemIsBeingCarried = false;
            Dead = true;
            if (WhichHand == 1)
            {
                Name.transform.position = HandLeft.transform.position;
                GlobalHeldLeft = false;
            }
            if (WhichHand == 2)
            {
                Name.transform.position = HandRight.transform.position;
                GlobalHeldRight = false;
            }
            HeldLeft = 0;
            HeldRight = 0;
            WhichHand = 0;
            Name.transform.position = Burner.transform.position;
            Name.transform.rotation = Burner.transform.rotation;
            Name.transform.SetParent(Burner.transform);
            ThisRigidBody.isKinematic = false;
            Name.transform.localPosition = new Vector3(0f, 4f, 0f);
            ThisRigidBody.velocity = new Vector3(0f, -0.25f, 0f);
            Destroy(this.gameObject,2.5f);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Character_Model_01")
        {
            HeldLeft--;
            HeldRight--;
        }
    }

    private void PickupLeft()
    {
        ThisRigidBody.useGravity = false;
        ThisRigidBody.isKinematic = true;
        Name.transform.position = HandLeft.transform.position;
        Name.transform.rotation = HandLeft.transform.rotation;
        Name.transform.SetParent(HandLeft.transform);
        HeldRight = 0;
    }
    private void PickupRight()
    {
        ThisRigidBody.useGravity = false;
        ThisRigidBody.isKinematic = true;
        Name.transform.position = HandRight.transform.position;
        Name.transform.rotation = HandRight.transform.rotation;
        Name.transform.SetParent(HandRight.transform);
        HeldLeft = 0;
    }
    private void Drop()
    {
        ThisItemIsBeingCarried = false;
        ThisRigidBody.useGravity = true;
        ThisRigidBody.isKinematic = false;
        transform.parent = null;
        if(WhichHand == 1)
        {
            Name.transform.position = HandLeft.transform.position;
            GlobalHeldLeft = false;
        }
        if(WhichHand == 2)
        {
            Name.transform.position = HandRight.transform.position;
            GlobalHeldRight = false;
        }
        HeldLeft = 0;
        HeldRight = 0;
        WhichHand = 0;
    }
}
