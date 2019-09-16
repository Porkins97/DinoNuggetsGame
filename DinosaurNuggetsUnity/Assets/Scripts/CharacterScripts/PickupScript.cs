using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    private bool RH_Carrying = false;
    private bool LH_Carrying = false;
    private bool RH_Hover = false;
    private bool LH_Hover = false;

    private GameObject RH_Object;
    private GameObject LH_Object;
    private GameObject RH;
    private GameObject LH;


    public void PickItUp(GameObject hand, GameObject obj)
    {
        if(hand.name == "RightHand")
        {
            RH_Hover = true;
            RH_Object = obj;
            RH = hand;
        }
        else if(hand.name == "LeftHand")
        {
            LH_Hover = true;
            LH_Object = obj;
            LH = hand;
        }
    }

    public void PickItUpExit(GameObject hand, GameObject obj)
    {
        if(hand.name == "RightHand")
        {
            RH_Hover = false;
            RH_Object = null;
            RH = null;
        }
        else if(hand.name == "LeftHand")
        {
            LH_Hover = false;
            LH_Object = null;
            LH = null;
        }
    }

    protected virtual void  Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(RH_Hover && RH_Object != null) 
            {
                RH_Object.GetComponent<Rigidbody>().isKinematic = true;
                RH_Object.transform.SetParent(RH.transform);
                RH_Object.transform.localPosition = new Vector3(0,0,0);
            }
        }
        else
        {
            if(RH_Object != null)
            {
                RH_Object.GetComponent<Rigidbody>().isKinematic = false;
                RH_Object.transform.SetParent(null);
            }
        }
        
        if (Input.GetMouseButton(1))
        {
            if(LH_Hover && LH_Object != null)
            {
                LH_Object.GetComponent<Rigidbody>().isKinematic = true;
                LH_Object.transform.SetParent(LH.transform);
                LH_Object.transform.localPosition = new Vector3(0,0,0);
            }
        }
        else
        {
            if(LH_Object != null)
            {
                LH_Object.GetComponent<Rigidbody>().isKinematic = false;
                LH_Object.transform.SetParent(null);
            }
        }
    }
}
