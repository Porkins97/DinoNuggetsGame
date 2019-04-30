using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public bool L_GlobalHeldLeft = false;
    public bool L_GlobalHeldRight = false;
    public bool L_OvenInUse = false;
    public bool L_PickUpOven = false;

    private int Pickup = 0;
    public GameObject OvenObject;
    public GameObject Player;
    private float Distance; 

    // Start is called before the first frame update
    void Start()
    {
       Player = GameObject.Find("Character_Model_01");
       OvenObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(OvenObject.transform.position, Player.transform.position);

        if ((Input.GetKeyDown(KeyCode.E)) || (Input.GetKeyDown(KeyCode.R)))
        {
            Pickup++;
        }

        if ((Input.GetKeyUp(KeyCode.E)) || (Input.GetKeyUp(KeyCode.R)))
        {
            Pickup--;
        } 
       
        if(Distance <= 1)
        {
            L_OvenInUse = OvenUtensil.OvenInUse;
        }
            

        if (L_OvenInUse == true)
        {
            Pickup++;
            Debug.Log("Distance is less than 1");
        }

        if (Pickup >= 2)
        {
           OvenUtensil.PickUpOven = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Character_Model_01")
        {
            Pickup--;
            Debug.Log("You Should not be able to pick this up");
        }
    }
}
