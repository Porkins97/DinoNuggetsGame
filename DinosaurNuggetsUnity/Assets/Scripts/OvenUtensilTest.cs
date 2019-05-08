using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenUtensilTest : MonoBehaviour
{
    public static bool OvenInUse = false;
    public static bool PickUpOven = false;

    public bool ThisIsOnOven = false;

    private Rigidbody ThisRigidBody = null;
    public GameObject Burner;
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
        Character = GameObject.Find("Character_Model_001");
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
        if ((collision.gameObject.tag == "Oven") && OvenInUse == false)
        {
            gameObject.GetComponent<PickupTest>().ThisItemIsBeingCarried = false;
            OvenInUse = true;
            Debug.Log("Oven in use = " + OvenInUse);
            Name.transform.position = Burner.transform.position;
            Name.transform.rotation = Burner.transform.rotation;
            Name.transform.SetParent(Burner.transform);

            if (gameObject.GetComponent<PickupTest>().WhichHand == 1)
            {
                Character.GetComponent<StaticBoolScript>().GlobalHeldLeft = false;
                Debug.Log("Test GlobalHeldLeft (Utensil Script) = " +Character.GetComponent<StaticBoolScript>().GlobalHeldLeft);
                gameObject.GetComponent<PickupTest>().HeldLeft = 0;
            }
            if (gameObject.GetComponent<PickupTest>().WhichHand == 2)
            {
                Character.GetComponent<StaticBoolScript>().GlobalHeldRight = false;
                Debug.Log("GlobalHeldRight (Utensil Script) = false");
                gameObject.GetComponent<PickupTest>().HeldRight = 0;
            }
            gameObject.GetComponent<PickupTest>().WhichHand = 0;

            ThisIsOnOven = true;
        }
    }
}