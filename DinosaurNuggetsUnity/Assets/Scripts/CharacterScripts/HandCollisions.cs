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
        Debug.Log("hit");
        if(col.tag == "Ingredient" || col.tag == "Utensil")
        {
            
            pickupScript.PickItUp(gameObject, col.gameObject);
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
