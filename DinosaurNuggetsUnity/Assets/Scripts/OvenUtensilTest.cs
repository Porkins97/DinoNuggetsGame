using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenUtensilTest : MonoBehaviour
{
    public static bool GlobalHeldLeft = false;
    public static bool GlobalHeldRight = false;

    public static bool PickUpOven = false;

    public bool ThisIsOnOven = false;
    private bool OnCupboard = false;

    private Rigidbody ThisRigidBody = null;
    public GameObject Burner;
    public GameObject Name;
    public GameObject Character;
    public GameObject CupboardTest;
    public GameObject Cupboard;
    public GameObject Cupboard1;
    public GameObject Oven;
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
        Character = GameObject.Find("Character_Model_01");
        Burner = GameObject.Find("Oven_001/Burner");
        PickupTest PickupScript = this.gameObject.GetComponent<PickupTest>();
        StaticBoolScript StaticBool = GameObject.Find("Character_Model_01").GetComponent<StaticBoolScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Oven")
        {
            Oven = collision.transform.root.gameObject;
            Debug.Log("Hit Oven and OvenInUse = " +Oven.gameObject.GetComponent<Oven>().OvenInUse);
        }

        if ((collision.gameObject.tag == "Oven") && Oven.gameObject.GetComponent<Oven>().OvenInUse == false)
        {
            gameObject.GetComponent<PickupTest>().ThisItemIsBeingCarried = false;
            //OvenInUse = true;
            Debug.Log("Oven in use = " +Oven.gameObject.GetComponent<Oven>().OvenInUse);
            Name.transform.position = Burner.transform.position;
            Name.transform.rotation = Burner.transform.rotation;
            Name.transform.SetParent(Burner.transform);

            if (gameObject.GetComponent<PickupTest>().WhichHand == 1)
            {
                GlobalHeldLeft = false;
                Debug.Log("Test GlobalHeldLeft (Utensil Script) = " +GlobalHeldLeft);
                gameObject.GetComponent<PickupTest>().HeldLeft = 0;
            }
            if (gameObject.GetComponent<PickupTest>().WhichHand == 2)
            {
                GlobalHeldRight = false;
                Debug.Log("GlobalHeldRight (Utensil Script) = false");
                gameObject.GetComponent<PickupTest>().HeldRight = 0;
            }
            gameObject.GetComponent<PickupTest>().WhichHand = 0;

            ThisIsOnOven = true;
        }
        if ((collision.gameObject.tag == "Cupboard") && (gameObject.GetComponent<PickupTest>().ThisItemIsBeingCarried == true))
        {
            CupboardTest = collision.transform.root.gameObject;
            Cupboard = CupboardTest.transform.Find("CuttingSpot").gameObject;
            Cupboard1 = CupboardTest.transform.Find("CuttingSpot (1)").gameObject;
            DoubleCupboard DoubleCupboardScript = CupboardTest.gameObject.GetComponent<DoubleCupboard>();
            if ((CupboardTest.GetComponent<DoubleCupboard>().Spot1 == false) || (CupboardTest.GetComponent<DoubleCupboard>().Spot1 == false))
            {
                OnCupboard = true;
                Drop();
                Debug.Log("Ingredient on cupboard");
            }
        }
    }
    private void Drop()
    {
        this.gameObject.GetComponent<PickupTest>().ThisItemIsBeingCarried = false;

        if (CupboardTest.GetComponent<DoubleCupboard>().Spot1 == false)
        {
            //ThisRigidBody.isKinematic = true;
            Name.transform.position = Cupboard.transform.position;
            Name.transform.rotation = Cupboard.transform.rotation;
            Name.transform.SetParent(Cupboard.transform);
            Debug.Log("Error Travis");
        }

        if (CupboardTest.GetComponent<DoubleCupboard>().Spot2 == false)
        {
            //ThisRigidBody.isKinematic = true;
            Name.transform.position = Cupboard1.transform.position;
            Name.transform.rotation = Cupboard1.transform.rotation;
            Name.transform.SetParent(Cupboard1.transform);
        }

        //if not on cupboard, it is assumed that it is in the pot. Dead only toggles on collider with oven. Could tidy this code a bit 
        //but it is functional for now.

    }
}