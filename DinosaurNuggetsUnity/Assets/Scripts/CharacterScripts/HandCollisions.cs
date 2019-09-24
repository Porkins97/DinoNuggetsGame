using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandCollisions : MonoBehaviour
{
    private Collider currentCollider;
    public PickupScript pickupScript;
    void Start()
    {
        currentCollider = gameObject.GetComponent<Collider>();
    }

    void OnTriggerEnter (Collider col) 
    {
        if(col.tag == "Ingredient" || col.tag == "Utensil")
        {
           if (col.GetComponent<BeingUsed>().beingUsed == false && col.GetComponent<BeingUsed>().Locked == false)
           {
                pickupScript.PickItUp(gameObject, col.gameObject);
           }
        }
    }
    void OnTriggerStay (Collider col) 
    {
        if(col.tag == "Ingredient" || col.tag == "Utensil")
        {
           if (col.GetComponent<BeingUsed>().beingUsed == false && col.GetComponent<BeingUsed>().Locked == false)
           {
                pickupScript.PickItUp(gameObject, col.gameObject);
           }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Ingredient" || col.tag == "Utensil")
        {
            pickupScript.PickItUpExit(gameObject, col.gameObject);
        }
    }
}
