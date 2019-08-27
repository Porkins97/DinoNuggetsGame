using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollisions : MonoBehaviour
{
    private Collider boxCol;
    void start()
    {
        boxCol = gameObject.GetComponent<Collider>();
    }
    void OnTriggerEnter (Collider col) 
    {
        if(col.tag == "Ingredient")
        {
            
        }
        //if (collider == playerCollider) CollideWithPlayer();
    }
}
