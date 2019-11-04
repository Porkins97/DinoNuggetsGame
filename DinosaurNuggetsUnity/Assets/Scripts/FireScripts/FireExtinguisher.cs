using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    public GameObject streamFX;
    public Rigidbody rigidBody;
    public ItemAttributes itemAttributes;
    private void Start() 
    {
        streamFX.SetActive(false);
        itemAttributes = GetComponent<ItemAttributes>();
    }
    
    private void Update() 
    {
        if(itemAttributes.beingUsed == true)
        {
            streamFX.SetActive(true);
            rigidBody.constraints = RigidbodyConstraints.None;
        }
        else
        {
            streamFX.SetActive(false);
        }
    }
}
