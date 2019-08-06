using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{

    public GameObject TrashCan;
    public GameObject Ingredient;
    public GameObject Position;
    Rigidbody IngredientRigidBody;

    Collider ThisCollider;

    // Start is called before the first frame update
    void Start()
    {
        TrashCan = this.gameObject;
        ThisCollider = this.gameObject.GetComponent<Collider>();
        Ingredient = null;
        Position = TrashCan.gameObject.transform.GetChild(0).gameObject;
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
            IngredientRigidBody = Ingredient.GetComponent<Rigidbody>();

            if(Ingredient.GetComponent<PickupTest>().ThisItemIsBeingCarried == true)
            {
                Debug.Log("This Item is being carried");
                Ingredient.GetComponent<PickupTest>().Drop();
            }
            IngredientRigidBody.isKinematic = false;
            Ingredient.GetComponent<Collider>().enabled = false;
            Ingredient.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Ingredient.transform.position = Position.transform.position;
            IngredientRigidBody.velocity = new Vector3(0f, -0.1f, 0f);

            GameObject.Destroy(Ingredient,2.00f);
            
        }
    }
}
