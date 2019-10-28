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
        Ingredient = null;
        //Position = TrashCan.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col !=null && col.tag == "Ingredient" && col.GetComponent<ItemAttributes>().beingUsed == false)
        {
            Debug.Log("Hit Bin");
            Ingredient = col.gameObject;
            IngredientRigidBody = Ingredient.GetComponent<Rigidbody>();
            IngredientRigidBody.isKinematic = false;
            //Ingredient.GetComponent<Collider>().enabled = false;
            //Ingredient.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //Ingredient.transform.position = Position.transform.position;
            //IngredientRigidBody.velocity = new Vector3(0f, -0.1f, 0f);

            //NEEDS to be removed from player list!!!
            //DinoPlayerSettings _player =  col.GetComponent<ItemAttributes>().lastPlayer
            //_player.dinoPlayer.GetComponent<PickupScript>().AllItems.Remove(obj);

            GameObject.Destroy(Ingredient,2.00f);
            
        }
    }
}
