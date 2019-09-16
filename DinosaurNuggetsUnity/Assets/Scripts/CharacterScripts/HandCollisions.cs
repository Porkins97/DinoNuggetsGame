using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandCollisions : MonoBehaviour
{
    private Collider boxCol;
    public PickupScript pickupScript;
    void Start()
    {
        boxCol = gameObject.GetComponent<Collider>();
    }

    void OnTriggerEnter (Collider col) 
    {
        if(col.tag == "Ingredient")
        {
            pickupScript.PickItUp(gameObject, col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Ingredient")
        {
            pickupScript.PickItUpExit(gameObject, col.gameObject);
        }
    }
}
