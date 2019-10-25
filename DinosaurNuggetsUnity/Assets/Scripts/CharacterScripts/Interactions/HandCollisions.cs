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

        pickupScript.AllItems = new List<GameObject>();
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Utensil")
        {
            SendPickupArgs(col);
        }
        else if (col.tag == "Ingredient")
        {
            SendPickupArgs(col);
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Utensil")
        {
            SendPickupArgs(col);
        }
        else if (col.tag == "Ingredient")
        {
            SendPickupArgs(col);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Ingredient" || col.tag == "Utensil")
        {
            pickupScript.PickItUpExit(gameObject);
            if (pickupScript.AllItems.Contains(col.gameObject))
            {
                pickupScript.AllItems.Remove(col.gameObject);
            }
        }
    }

    private void SendPickupArgs(Collider col)
    {
        if (col.GetComponent<ItemAttributes>().beingUsed == false)
        {
            if (!pickupScript.AllItems.Contains(col.gameObject))
            {
                pickupScript.AllItems.Add(col.gameObject);
                pickupScript.AllItems.Sort(EnumLibrary.IngredientsOverAll);
                pickupScript.PickItUp(gameObject, pickupScript.AllItems[0], pickupScript.AllItems[0].GetComponent<ItemAttributes>().Locked, pickupScript.AllItems[0].GetComponent<ItemAttributes>().unlockTime);
            }
        }
    }
}
