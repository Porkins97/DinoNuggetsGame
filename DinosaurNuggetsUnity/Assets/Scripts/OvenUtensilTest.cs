using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenUtensilTest : MonoBehaviour
{
    public static bool OvenInUse = false;
    public static bool PickUpOven = false;
    public static bool GlobalHeldLeft = false;
    public static bool GlobalHeldRight = false;

    public bool ThisIsOnOven = false;

    private Rigidbody ThisRigidBody = null;
    public GameObject Burner;
    public GameObject Name;
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
        Burner = GameObject.Find("Oven_001/Burner");
        PickupTest PickupScript = this.gameObject.GetComponent<PickupTest>();
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
                GlobalHeldLeft = false;
                Debug.Log("Test GlobalHeldLeft = " +GlobalHeldLeft);
            }
            if (gameObject.GetComponent<PickupTest>().WhichHand == 2)
            {
                GlobalHeldRight = false;
                Debug.Log("GlobalHeldRight = false");
            }
            gameObject.GetComponent<PickupTest>().WhichHand = 0;

            ThisIsOnOven = true;
        }
    }
}