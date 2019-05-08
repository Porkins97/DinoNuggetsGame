using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTest : MonoBehaviour
{
    public static bool GlobalHeldLeft;
    public static bool GlobalHeldRight;

    //These are global variables. all objects using scripts with these variables know if they are true or false. this is so that.
    //the player cannot pick up more than one thing per hand.
    public bool ThisItemIsBeingCarried = false;
   
    //tells which hand the object is in and if there is something being held
    public int HeldLeft = 0;
    public int HeldRight = 0;
    public int WhichHand = 0;

    private Rigidbody ThisRigidBody = null;

    //the different game objects that this script can interact with. Name is this item in particular
    public GameObject HandLeft;
    public GameObject HandRight;
    public GameObject Name;
    public GameObject Character;

    Collider ObjectCollider;

    // Start is called before the first frame update
    void Start()
    {
        ThisRigidBody = GetComponent<Rigidbody>();

        //Fetch the GameObject's Collider (make sure they have a Collider component)
        ObjectCollider = GetComponent<Collider>();

        //Here the GameObject's Collider is not a trigger
        ObjectCollider.isTrigger = false;

        //initialise which game objects are connected
        Name = this.gameObject;
        HandLeft = GameObject.Find("Character_Model_01/HandLeft");
        HandRight = GameObject.Find("Character_Model_01/HandRight");
        Character = GameObject.Find("Character_Model_01");
        OvenUtensilTest OvenUtensilScript = this.gameObject.GetComponent<OvenUtensilTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && ThisItemIsBeingCarried == true)
        {
            Drop();
            Debug.Log("Drop");
        }

        /*if the item is to be picked up, it needs to meet two conditions: it is in contact with the player controller AND the player is pressing a key
        this is accomplished by using a two part boolean in the form of an int (HeldLeft and HeldRight). for each of the conditions, the int is increased
        by one, so the item can only be interacted with when the int == 2 */

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HeldLeft++;
            Debug.Log("Left Mouse Down and left hand = " +GlobalHeldLeft);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            HeldRight++;
            Debug.Log("Right Mouse Down and Right Hand =" +GlobalHeldRight);
        }

        //condition = false
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            HeldLeft--;
            //Drop();

        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            HeldRight--;
            //Drop();
        }


        if ((HeldLeft == 2) && (GlobalHeldLeft == false))
        {
            Debug.Log("Held Left == " +HeldLeft);
            Debug.Log("and Global Held Left == " +GlobalHeldLeft);
            //This is triggering and preventing the player from picking something else up, after the object is placed on the oven
            //i think the issue is because it is being called in the update part, if there's another way to do it the problem will be fixed
            GlobalHeldLeft = true;
            Debug.Log("Global Held Left Triggered True");
            ThisItemIsBeingCarried = true;
            PickupLeft();
            WhichHand = 1;
        }
        if ((HeldRight == 2) && (GlobalHeldRight == false))
        {
            GlobalHeldRight = true;
            Debug.Log("Global Held Right Triggered True");
            ThisItemIsBeingCarried = true;
            PickupRight();
            WhichHand = 2;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character_Model_01")
        {
            Debug.Log("Hit Character Collider");
            HeldLeft++;
            HeldRight++;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Character_Model_01")
        {
            Debug.Log("Left Character Collider");
            HeldLeft--;
            HeldRight--;
        }
    }

    private void PickupLeft()
    {
        Debug.Log("Pickup Left Triggered");
        ThisRigidBody.useGravity = false;
        ThisRigidBody.isKinematic = true;
        Name.transform.position = HandLeft.transform.position;
        Name.transform.rotation = HandLeft.transform.rotation;
        Name.transform.SetParent(HandLeft.transform);
        Debug.Log("PickUp Left Finished");
    }
    private void PickupRight()
    {
        Debug.Log("PickUpRight Triggered");
        ThisRigidBody.useGravity = false;
        ThisRigidBody.isKinematic = true;
        Name.transform.position = HandRight.transform.position;
        Name.transform.rotation = HandRight.transform.rotation;
        Name.transform.SetParent(HandRight.transform);
        Debug.Log("PickUpRight Finished");
    }
    private void Drop()
    {
        Debug.Log("Drop Triggered");
        ThisItemIsBeingCarried = false;
        ThisRigidBody.isKinematic = false;

        if (WhichHand == 1)
        {
            Name.transform.position = HandLeft.transform.position;
            GlobalHeldLeft = false;
            Debug.Log("LeftHand Drop");
        }

        if (WhichHand == 2)
        {
            Name.transform.position = HandRight.transform.position;
            GlobalHeldRight = false;
            Debug.Log("RightHand Drop");
        }

        HeldLeft = 0;
        HeldRight = 0;
        WhichHand = 0;

        ThisRigidBody.useGravity = true;
        transform.parent = null;
        Debug.Log("Drop Finished");
    }
}