using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove_Collider : MonoBehaviour
{
    [SerializeField] private Stove stoveScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Utensil")
        {
            if (col.GetComponent<BeingUsed>().beingUsed == false)
            {
                stoveScript.Placed(col.gameObject);
            }
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Utensil")
        {
            if (col.GetComponent<BeingUsed>().beingUsed == false)
            {
                stoveScript.Placed(col.gameObject);
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Utensil")
        {
            if (col.GetComponent<BeingUsed>().beingUsed == true)
            {
                stoveScript.Removed(col.gameObject);
            }
        }
    }
}
