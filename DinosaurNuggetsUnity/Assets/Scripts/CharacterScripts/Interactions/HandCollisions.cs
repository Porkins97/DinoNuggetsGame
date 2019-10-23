using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandCollisions : MonoBehaviour
{
    private Collider currentCollider;
    private PickupScript pickupScript;
    void Start()
    {
        currentCollider = gameObject.GetComponent<Collider>();
        pickupScript = GetComponentInParent<PickupScript>();
    }

    void OnTriggerEnter (Collider col) 
    {
        if(col.tag == "Ingredient" || col.tag == "Utensil")
        {
           if (col.GetComponent<ItemAttributes>().beingUsed == false)
           {
                pickupScript.PickItUp(gameObject, col.gameObject, col.GetComponent<ItemAttributes>().Locked);
           }
        }
    }
    void OnTriggerStay (Collider col) 
    {
        if(col.tag == "Ingredient" || col.tag == "Utensil")
        {
           if (col.GetComponent<ItemAttributes>().beingUsed == false)
           {
                pickupScript.PickItUp(gameObject, col.gameObject, col.GetComponent<ItemAttributes>().Locked);
           }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Ingredient" || col.tag == "Utensil")
        {
            pickupScript.PickItUpExit(gameObject);
        }
    }
}
