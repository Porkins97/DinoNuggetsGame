using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        else if(hand.name == "LeftHand" && LeftHand_Carrying == false)
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
        else if(hand.name == "LeftHand" && LeftHand_Carrying == false)
        {
            LeftHand_Hover = false;
            LeftHand_Object = null;
            LeftHand = null;
        }
    }

    private void  Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(RightHand_Hover && RightHand_Object != null && !RightHand_Carrying) 
            {
                RightHand_Object.GetComponent<Rigidbody>().isKinematic = true;
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
                RightHand_Object.GetComponent<Rigidbody>().isKinematic = false;
                RightHand_Object.transform.SetParent(null);
                RightHand_Carrying = false;
                RightHand_Object.GetComponent<BeingUsed>().beingUsed = false;
            }
        }
        
        if (Input.GetMouseButton(1))
        {
            if (LeftHand_Hover && LeftHand_Object != null && !LeftHand_Carrying)
            {
                LeftHand_Object.GetComponent<Rigidbody>().isKinematic = true;
                LeftHand_Object.transform.SetParent(LeftHand.transform);
                LeftHand_Object.transform.localPosition = new Vector3(0, 0, 0);
                LeftHand_Carrying = true;
                RightHand_Object.GetComponent<BeingUsed>().beingUsed = true;
            }
        }
        else
        {
            if(LeftHand_Object != null && LeftHand_Carrying)
            {
                LeftHand_Object.GetComponent<Rigidbody>().isKinematic = false;
                LeftHand_Object.transform.SetParent(null);
                LeftHand_Carrying = false;
                RightHand_Object.GetComponent<BeingUsed>().beingUsed = false;
            }
        }
    }
}
