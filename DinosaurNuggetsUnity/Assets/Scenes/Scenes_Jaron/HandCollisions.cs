using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandCollisions : MonoBehaviour
{
    [SerializeField] private GameObject PickupGO = null;
    private Quaternion _iniRot;

    void Start()
    {
        _iniRot = transform.rotation;
    }
    
    void LateUpdate()
    {
        transform.rotation = _iniRot;
    }



    
    void OnTriggerEnter (Collider col) 
    {
        if(col.tag == "Ingredient")
        {
            PickupGO.GetComponent<PickupScript>().PickItUp(gameObject, col.gameObject);
        }
    }
    void OnTriggerExit (Collider col) 
    {
        if(col.tag == "Ingredient")
        {
            PickupGO.GetComponent<PickupScript>().PickItUpExit(gameObject, col.gameObject);
        }
    }
    

    


}
