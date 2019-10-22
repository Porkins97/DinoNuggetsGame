using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DinoInputSystems;

[RequireComponent(typeof(DinoInputSystem))]
public class PickupScript : MonoBehaviour
{

    //Private Variables
    private bool RightHand_Carrying = false;
    private bool LeftHand_Carrying = false;
    private bool RightHand_Hover = false;
    private bool LeftHand_Hover = false;
    
    private bool RightHand_Object_Locked;
    private bool LeftHand_Object_Locked;

    private GameObject RightHand_Object;
    private GameObject LeftHand_Object;
    private GameObject RightHand;
    private GameObject LeftHand;

    private DinoInputSystem _DIS;

    //Inherited variables
    private bool dinoRightHand { get { return _DIS.dinoRightHand; } }
    private bool dinoLeftHand { get { return _DIS.dinoLeftHand; } }

    void Start()
    {
        _DIS = GetComponent<DinoInputSystem>();
    }


    public void PickItUp(GameObject hand, GameObject obj, bool locked)
    {
        if(hand.name == "RightHand" && RightHand_Carrying == false)
        {
            RightHand_Hover = true;
            RightHand_Object = obj;
            RightHand = hand;
            RightHand_Object_Locked = locked;
        }
        if(hand.name == "LeftHand" && LeftHand_Carrying == false)
        {
            LeftHand_Hover = true;
            LeftHand_Object = obj;
            LeftHand = hand;
            LeftHand_Object_Locked = locked;
        }
    }

    public void PickItUpExit(GameObject hand)
    {
        if(hand.name == "RightHand" && RightHand_Carrying == false)
        {
            RightHand_Hover = false;
            RightHand_Object = null;
            RightHand = null;
        }
        if(hand.name == "LeftHand" && LeftHand_Carrying == false)
        {
            LeftHand_Hover = false;
            LeftHand_Object = null;
            LeftHand = null;
        }
    }
    
    private void Update()
    {
        if (dinoRightHand)
        {
            if (RightHand_Object_Locked)
                if(Unlock())
                    PickUpItem(ref RightHand, ref RightHand_Carrying, ref RightHand_Object, RightHand_Hover);
        }
        else
        {
            DropItem(ref RightHand_Carrying, ref RightHand_Object);
        }
        
        if (dinoLeftHand)
        {
            PickUpItem(ref LeftHand, ref LeftHand_Carrying, ref LeftHand_Object, LeftHand_Hover);
        }
        else
        {
            DropItem(ref LeftHand_Carrying, ref LeftHand_Object);
        }
    }

    private void PickUpItem(ref GameObject hand, ref bool carrying, ref GameObject obj, bool hovering)
    {
        if (hovering && obj != null && !carrying)
        {
            //Set Rigidbody off - Turn on Kinematic and set contraints to none.
            obj.GetComponent<Rigidbody>().isKinematic = true;
            obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            //Set the parent and reset transform and rotation to the parents.
            obj.transform.SetParent(hand.transform);
            obj.transform.localPosition = new Vector3(0, 0, 0);
            obj.transform.localRotation = Quaternion.identity;

            //Set the carrying hand to true, and make sure the items attribute is being used.
            carrying = true;
            obj.GetComponent<ItemAttributes>().beingUsed = true;
        }
    }
    private void DropItem(ref bool carrying, ref GameObject obj)
    {
        if (obj != null && carrying)
        { 
            //Set Rigidbody back
            obj.GetComponent<Rigidbody>().isKinematic = false;

            //Remove the parents
            obj.transform.SetParent(obj.GetComponent<ItemAttributes>().initialParent);

            //Make sure all things are now gotten rid of.
            carrying = false;
            obj.GetComponent<ItemAttributes>().beingUsed = false;
        }
    }
    private bool Unlock()
    {
        bool returnVal = false;

        return returnVal;
    }
}
