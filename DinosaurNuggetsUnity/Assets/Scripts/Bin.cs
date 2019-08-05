using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{

    public GameObject TrashCan;
    public GameObject Ingredient;

    Collider ThisCollider;

    // Start is called before the first frame update
    void Start()
    {
        TrashCan = this.gameObject;
        ThisCollider = this.gameObject.GetComponent<Collider>();
        Ingredient = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ingredient")
        {
            Debug.Log("Hit Bin");
            Ingredient = collision.gameObject;

            if(Ingredient.GetComponent<PickupTest>().ThisItemIsBeingCarried == true)
            {
                Debug.Log("This Item is being carried");
                Ingredient.GetComponent<PickupTest>().Drop();
            }

            GameObject.Destroy(Ingredient);

        }
    }
}
