using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientTest : MonoBehaviour
{ 
    //States whether you can cut the object on the cupboard. Pulled value from DoubleCupboard script.
    public static bool Cut = false;
    public static bool CupboardInUse = false;
    public static bool OvenInUse;

    private bool Dead = false;
    private bool OnCupboard = false;

    //Shrink Speed and Scale
    public float TargetSpeed = 0.001f;
    public float ShrinkSpeed = 0.1f;

    public GameObject CupboardTest;
    public GameObject Cupboard;
    public GameObject Cupboard1;
    public GameObject Knife;
    public GameObject Name;
    public GameObject CutIngredient;
    public GameObject Burner;
    public GameObject Oven;
    public GameObject Character;
    public GameObject Position;

    Collider ObjectCollider;
    private Rigidbody ThisRigidBody = null;
    // Start is called before the first frame update
    void Start()
    {
        Knife = GameObject.Find("Knife_001");
        CutIngredient = GameObject.Find("CutTomato_001");
        Character = GameObject.Find("Character_Model_01");
        Name = this.gameObject;
        ThisRigidBody = GetComponent<Rigidbody>();
        ObjectCollider = GetComponent<Collider>();
        CupboardTest = null;

        PickupTest PickupScript = this.gameObject.GetComponent<PickupTest>();
        OvenUtensilTest OvenUtensilScript = this.gameObject.GetComponent<OvenUtensilTest>();

    }
    // Update is called once per frame
    void Update()
    {
        if(Dead == true)
        {
            //reduce in size and shrink as it descends into the pot
            Name.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //Name.transform.localScale = Vector3.Lerp (Name.transform.localScale, new Vector3(TargetScale, TargetScale, TargetScale), Time.deltaTime * ShrinkSpeed);
            ObjectCollider.enabled = false;
        }

       /* if((OnCupboard == true) && (DoubleCupboard.Cut == true))
        {
            Instantiate(CutIngredient);
            CutIngredient.transform.position = Cupboard.transform.position;
            CutIngredient.transform.SetParent(Cupboard.transform);
            Destroy(this.gameObject);
        } */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Oven")
        {
            Debug.Log("Ingredient Hit Oven");
            Oven = collision.gameObject;
            Burner = Oven.gameObject.transform.GetChild(0).gameObject;
            Position = Oven.gameObject.transform.GetChild(1).gameObject;
        
            if(Oven.gameObject.GetComponent<Oven>().OvenInUse == true)
            {
                Dead = true;
                Drop();
            }
        }

        if((collision.gameObject.tag == "Cupboard") && (gameObject.GetComponent<PickupTest>().ThisItemIsBeingCarried == true))
        {
            CupboardTest = collision.gameObject;
            Cupboard = CupboardTest.transform.Find("CuttingSpot1").gameObject;
            Cupboard1 = CupboardTest.transform.Find("CuttingSpot2").gameObject;
            DoubleCupboard DoubleCupboardScript = CupboardTest.gameObject.GetComponent<DoubleCupboard>();
            if ((CupboardTest.GetComponent<DoubleCupboard>().Spot1 == false)||(CupboardTest.GetComponent<DoubleCupboard>().Spot2 == false))
            {
                OnCupboard = true;
                Drop();
                Debug.Log("Ingredient on cupboard");
            }
        }
    }

    private void Drop()
    {
        this.gameObject.GetComponent<PickupTest>().ThisItemIsBeingCarried = false;

        if (Dead == true)
        {
            ThisRigidBody.isKinematic = false;
            Debug.Log("Dead == " + Dead);
            Name.transform.position = Position.transform.position;
            Name.transform.rotation = Position.transform.rotation;
            Name.transform.SetParent(Burner.transform);
            //Name.transform.localPosition = new Vector3(0f, 1f, 0f);
            ThisRigidBody.velocity = new Vector3(0f, -0.5f, 0f);
            //Destroy(this.gameObject, 2.5f);
            Debug.Log("Ingredient in pot");

            //Test code to test out recipes
            Ingredient ingredient = GetComponent<Ingredient>();
            Recipe recipe = null;
            if (Character != null)
                recipe = Character.GetComponent<Recipe>();
            if (ingredient != null && recipe != null)
            {
                bool was_correct = recipe.AddIngredient(ingredient);
               // if (was_correct)
                    //Destroy(ingredient.gameObject);
            }
        }

        if (CupboardTest != null && CupboardTest.GetComponent<DoubleCupboard>().Spot1 == false)
        {
            //ThisRigidBody.isKinematic = true;
            Name.transform.position = Cupboard.transform.position;
            Name.transform.rotation = Cupboard.transform.rotation;
            Name.transform.SetParent(Cupboard.transform);
            Debug.Log("Error Travis");
        }

        if (CupboardTest != null && CupboardTest.GetComponent<DoubleCupboard>().Spot2 == false)
        {
        //ThisRigidBody.isKinematic = true;
        Name.transform.position = Cupboard1.transform.position;
        Name.transform.rotation = Cupboard1.transform.rotation;
        Name.transform.SetParent(Cupboard1.transform);
        }

        //if not on cupboard, it is assumed that it is in the pot. Dead only toggles on collider with oven. Could tidy this code a bit 
        //but it is functional for now.
        
    }
}