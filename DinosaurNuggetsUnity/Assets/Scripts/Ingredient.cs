using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    //These are global variables. all objects using scripts with these variables know if they are true or false. this is so that 
    //the player cannot pick up more than one thing per hand
    public static bool GlobalHeldLeft = false;
    public static bool GlobalHeldRight = false;
   
    //tells if there is an object on top of the oven. Only one oven, need to change to apply PER INSTANCE
    public static bool OvenInUse = false;
    public static bool PickUpOven = false;
    public static bool Cut = false;
    public static bool CupboardInUse = false;

    private bool ThisItemIsBeingCarried = false;
    private bool Dead = false;
    private bool OnCupboard = false;
    
    //tells which hand the object is in and if there is something being held
    private int HeldLeft = 0;
    private int HeldRight = 0;
    private int WhichHand = 0;

    //Shrink speed and scale
    public float TargetScale = 0.001f;
    public float ShrinkSpeed = 0.01f;

    private Rigidbody ThisRigidBody = null;

    //the different game objects that this script can interact with. Name is this item in particular
    public GameObject HandLeft;
    public GameObject HandRight;
    public GameObject Name;
    public GameObject Character;
    public GameObject Burner;
    public GameObject Cupboard;
    public GameObject Knife;

    public GameObject CutTomato;

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
       
        //initialise which game objects are connected
        Name = this.gameObject;
        HandLeft = GameObject.Find("Character_Model_01/HandLeft");
        HandRight = GameObject.Find("Character_Model_01/HandRight");
        Character = GameObject.Find("Character_Model_01");
        Burner = GameObject.Find("Oven_001/Burner");
        Cupboard = GameObject.Find("Cupboard_Double_001/CuttingSpot");
        Knife = GameObject.Find("Knife_001");
    }

    // Update is called once per frame
    void Update()
    {
        /*if the item is to be picked up, it needs to meet two conditions: it is in contact with the player controller AND the player is pressing a key
        this is accomplished by using a two part boolean in the form of an int (HeldLeft and HeldRight). for each of the conditions, the int is increased
        by one, so the item can only be interacted with when the int == 2 */
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HeldLeft++;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            HeldRight++;
        }

        //condition = false
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

        if (Dead == true)
        {
            //reduce in size and shrink as it decends into the pot
            Name.transform.localScale = new Vector3(0.15f, 0.7f, 0.15f);
            //Name.transform.localScale = Vector3.Lerp(Name.transform.localScale, new Vector3(TargetScale, TargetScale, TargetScale), Time.deltaTime * ShrinkSpeed);
            ObjectCollider.enabled = false;
        }

        if ((OnCupboard == true) && (DoubleCupboard.Cut == true))
        {
            Instantiate(CutTomato);
            CutTomato.transform.position = Cupboard.transform.position;
            CutTomato.transform.SetParent(Cupboard.transform);
            Destroy(this.gameObject);
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
            Dead = true;
            Drop();
        }

        if ((collision.gameObject.name == "Cupboard_Double_001") && (ThisItemIsBeingCarried == true))
        {
            OnCupboard = true;
            CupboardInUse = true;
            Drop();
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
        ThisRigidBody.isKinematic = false;

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

        if (OnCupboard == true)
        {
            ThisRigidBody.isKinematic = true;
            Name.transform.position = Cupboard.transform.position;
            Name.transform.rotation = Cupboard.transform.rotation;
            Name.transform.SetParent(Cupboard.transform);
        }

        if (Dead == false)
        {
            ThisRigidBody.useGravity = true;
            transform.parent = null;
        }
        else
        {
            Debug.Log("Destroy");
            Name.transform.position = Burner.transform.position;
            Name.transform.rotation = Burner.transform.rotation;
            Name.transform.SetParent(Burner.transform);
            Name.transform.localPosition = new Vector3(0f, 4f, 0f);
            ThisRigidBody.velocity = new Vector3(0f, -0.25f, 0f);
            Destroy(this.gameObject, 2.5f);
        }
    }
}
