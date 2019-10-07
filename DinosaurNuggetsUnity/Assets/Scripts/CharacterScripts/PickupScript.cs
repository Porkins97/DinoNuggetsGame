using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PickupScript : MonoBehaviour
{
    public bool RightHand_Carrying = false;
    public bool LeftHand_Carrying = false;
    private bool RightHand_Hover = false;
    private bool LeftHand_Hover = false;

    private GameObject RightHand_Object;
    private GameObject LeftHand_Object;
    private GameObject RightHand;
    private GameObject LeftHand;


    public void PickItUp(GameObject hand, GameObject obj)
    {
        if(hand.name == "RightHand" && RightHand_Carrying == false)
        {
            RightHand_Hover = true;
            RightHand_Object = obj;
            RightHand = hand;
        }
        if(hand.name == "LeftHand" && LeftHand_Carrying == false)
        {
            LeftHand_Hover = true;
            LeftHand_Object = obj;
            LeftHand = hand;
        }
    }

    public void PickItUpExit(GameObject hand, GameObject obj)
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
        if (Input.GetMouseButton(1))
        {
            if(RightHand_Hover && RightHand_Object != null && !RightHand_Carrying) 
            {
                RightHand_Object.GetComponent<Rigidbody>().isKinematic = true;
                RightHand_Object.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                RightHand_Object.transform.SetParent(RightHand.transform);
                RightHand_Object.transform.localPosition = new Vector3(0, 0, 0);

                RightHand_Carrying = true;
                RightHand_Object.GetComponent<BeingUsed>().beingUsed = true;
            }
        }
        else
        {
            if(RightHand_Object != null && RightHand_Carrying)
            {
                //Set Rigidbody back
                RightHand_Object.GetComponent<Rigidbody>().isKinematic = false;
                
                //Remove the sources
                RightHand_Object.transform.SetParent(RightHand_Object.GetComponent<BeingUsed>().initialParent);

                //Make sure all things are now gotten rid of.
                RightHand_Carrying = false;
                RightHand_Object.GetComponent<BeingUsed>().beingUsed = false;
            }
        }
        
        if (Input.GetMouseButton(0))
        {
            if (LeftHand_Hover && LeftHand_Object != null && !LeftHand_Carrying)
            {
                LeftHand_Object.GetComponent<Rigidbody>().isKinematic = true;
                LeftHand_Object.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                LeftHand_Object.transform.SetParent(LeftHand.transform);
                LeftHand_Object.transform.localPosition = new Vector3(0, 0, 0);

                LeftHand_Carrying = true;
                LeftHand_Object.GetComponent<BeingUsed>().beingUsed = true;
            }
        }
        else
        {
            if(LeftHand_Object != null && LeftHand_Carrying)
            {
                LeftHand_Object.GetComponent<Rigidbody>().isKinematic = false;
                LeftHand_Object.transform.SetParent(LeftHand_Object.GetComponent<BeingUsed>().initialParent);
                LeftHand_Carrying = false;
                LeftHand_Object.GetComponent<BeingUsed>().beingUsed = false;
            }
        }
    }
}
