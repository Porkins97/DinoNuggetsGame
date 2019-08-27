using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCupboard : MonoBehaviour
{
    public static bool Cut = false;
    public static bool GlobalHeldLeft;
    public static bool GlobalHeldRight;

    public bool Spot1 = false;
    public bool Spot2 = false;
    private int value1 = 0;
    private int value2 = 0;
    private bool Mouse0Down = false;
    private bool Mouse1Down = false;

    public GameObject Knife;
    public GameObject Cupboard;
    public GameObject Cuttingspot1;
    public GameObject Cuttingspot2;
    public GameObject Ingredient1;
    public GameObject Ingredient2;
    public GameObject HandLeft;
    public GameObject HandRight;
    public GameObject placeholder1;
    public GameObject placeholder2;
    public GameObject Name;
    public GameObject Player;

    private Rigidbody ThisRigidBody;
    private Rigidbody Rb;
    private Collider ThisCollider;
    private Collider IngredientCollider;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        Knife = GameObject.Find("Knife_001");
        Cupboard = this.gameObject;
        ThisRigidBody = GetComponent<Rigidbody>();
        ThisCollider = GetComponent<Collider>();
        if (Cuttingspot1 == null)
            Cuttingspot1 = this.gameObject.transform.Find("CuttingSpot").gameObject;
        if (Cuttingspot2 == null)
            Cuttingspot2 = this.gameObject.transform.Find("CuttingSpot (1)").gameObject;
        HandLeft = GameObject.Find("Character_Model_01/HandLeft");
        HandRight = GameObject.Find("Character_Model_01/HandRight");
        Player = GameObject.Find("Character_Model_01");

    }

    private void Update()
    {
        if (Player == null)
            return;
        float distance = Vector3.Distance(Cupboard.transform.position, Player.transform.position);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Mouse0Down = true;
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            Mouse0Down = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Mouse1Down = true;
        }

        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            Mouse1Down = false;
        }

        if (Cuttingspot1.transform.childCount == 1)
        {
            Spot1 = true;

        }
        else
        {
            Spot1 = false;
        }

        if (Cuttingspot2.transform.childCount == 1)
        {
            Spot2 = true;
        }
        else
        {
            Spot2 = false;
        }

        //Debug.Log("Distance = " + distance);

        if(distance <= 1.5f)
        {
            if (Mouse0Down == true)
            {
                Debug.Log("MouseKey0 Down");
                if ((Spot1 == true) || (Spot2 == true))
                {
                    //PickupLeft();
                }
            }

            if(Mouse1Down == true)
            {
                Debug.Log("MouseKey1 Down");
                if ((Spot1 == true) || (Spot2 == true))
                {
                    //PickupRight();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Knife_001")
        {
            Cut = true;
        }

        if (collision.gameObject.name == "Character_Model_01")
        {
            Debug.Log("Hit Cupboard");
            if (Mouse0Down == true)
            {
                Debug.Log("MouseKey0 Down");
                if ((Spot1 == true) || (Spot2 == true))
                {
                    PickupLeft();
                }
            }
            
            if(Mouse1Down == true)
            {
                Debug.Log("MouseKey1 Down");
                if ((Spot1 == true) || (Spot2 == true))
                {
                    PickupRight();
                }
            }
        }

        if (collision.gameObject.tag == "Ingredient")
        {
            if (Ingredient1 == null)
            {
                Ingredient1 = collision.gameObject;
            }
            else
            {
                if (Ingredient2 == null)
                {
                    Ingredient2 = collision.gameObject;
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.name == "Knife_001")
        {
            Cut = false;
        }
    }
    public void PickupLeft()
    {
        if(GlobalHeldLeft == false)
        {
            HandLeft = GameObject.Find("Character_Model_01/HandLeft");
            Debug.Log("Pickup Works");
            GlobalHeldLeft = true;

            if (Ingredient1 != null)
            {
                Name = Ingredient1.gameObject;
                Rb = Ingredient1.gameObject.GetComponent<Rigidbody>();
                Ingredient1 = null;
            }
            else
            {
                if (Ingredient2 != null)
                {
                    Name = Ingredient2.gameObject;
                    Rb = Ingredient2.gameObject.GetComponent<Rigidbody>();
                    Ingredient2 = null;
                }
            }
            Name.gameObject.GetComponent<PickupTest>().ThisItemIsBeingCarried = true;

            Rb.useGravity = false;
            Rb.isKinematic = true;
            Name.transform.position = HandLeft.transform.position;
            Name.transform.rotation = HandLeft.transform.rotation;
            Name.transform.SetParent(HandLeft.transform);
            Debug.Log("PickUp Left Finished");
        }
    }

    public void PickupRight()
    {
        if(GlobalHeldRight == false)
        {
            HandRight = GameObject.Find("Character_Model_01/HandRight");
            Debug.Log("Pickup Works");
            GlobalHeldRight = true;

            if (Ingredient1 != null)
            {
                Name = Ingredient1.gameObject;
                Rb = Ingredient1.gameObject.GetComponent<Rigidbody>();
                Ingredient1 = null;
            }
            else
            {
                if (Ingredient2 != null)
                {
                    Name = Ingredient2.gameObject;
                    Rb = Ingredient2.gameObject.GetComponent<Rigidbody>();
                    Ingredient2 = null;
                }
            }
            Name.gameObject.GetComponent<PickupTest>().ThisItemIsBeingCarried = true;

            Rb.useGravity = false;
            Rb.isKinematic = true;
            Name.transform.position = HandRight.transform.position;
            Name.transform.rotation = HandRight.transform.rotation;
            Name.transform.SetParent(HandRight.transform);
            Debug.Log("PickUp Right Finished");
        }
    }
        
}
