using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenUtensil : MonoBehaviour
{
    //if it is in the hand, Held = true
    public static bool GlobalHeldLeft = false;
    public static bool GlobalHeldRight = false;
    public static bool OvenInUse = false;
    public static bool PickUpOven = false;

    private bool ThisItemIsBeingCarried = false;
    private bool Dead = false; 

    private int HeldLeft = 0;
    private int HeldRight = 0;
    private int WhichHand = 0;

    private Rigidbody ThisRigidBody = null;
    public GameObject Burner;
    public GameObject HandLeft;
    public GameObject HandRight;
    public GameObject Name;
    public GameObject Character;
    Collider ObjectCollider;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Interactable";
        ThisRigidBody = GetComponent<Rigidbody>();
        //Fetch the GameObject's Collider (make sure they have a Collider component)
        ObjectCollider = GetComponent<Collider>();
        //Here the GameObject's Collider is not a trigger
        ObjectCollider.isTrigger = false;
        Name = this.gameObject;
        HandLeft = GameObject.Find("Character_Model_01/HandLeft");
        HandRight = GameObject.Find("Character_Model_01/HandRight");
        Character = GameObject.Find("Character_Model_01");
        Burner = GameObject.Find("Oven_001/Burner");
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && ThisItemIsBeingCarried == true)
        {
            Drop();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HeldLeft++;
            //this isn't working at the moment
            if ((PickUpOven == true) && (GlobalHeldLeft == false))
            {
                PickupLeft();
                PickUpOven = false;
                OvenInUse = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            HeldRight++;
            //this isn't working at the moment
            if((PickUpOven == true) && (GlobalHeldRight == false))
            {
                PickupRight();
                PickUpOven = false;
                OvenInUse = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            HeldLeft--;

            if(ThisItemIsBeingCarried == true)
            {
                Drop();
            }
            
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            HeldRight--;
            if(ThisItemIsBeingCarried == true)
            {
                Drop();
            }
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character_Model_01")
        {
            HeldLeft++;
            HeldRight++;
        }

        if ((collision.gameObject.tag == "Oven") && OvenInUse == false)
        {
            ThisItemIsBeingCarried = false;
            OvenInUse = true;
            Debug.Log("Oven in use = " + OvenInUse);
            Name.transform.position = Burner.transform.position;
            Name.transform.rotation = Burner.transform.rotation;
            Name.transform.SetParent(Burner.transform);
            HeldLeft = 0;
            HeldRight = 0;
            if (WhichHand == 1)
            {
                GlobalHeldLeft = false;
                Debug.Log("GlobalHeldLeft = false");
            }
            if (WhichHand == 2)
            {
                GlobalHeldRight = false;
                Debug.Log("GlobalHeldRight = false");
            }
            WhichHand = 0;
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
    }
}