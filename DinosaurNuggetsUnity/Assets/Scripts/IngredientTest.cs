using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientTest : MonoBehaviour
{
    //States whether you can cut the object on the cupboard. Pulled value from DoubleCupboard script.
    public static bool Cut = false;
    public static bool CupboardInUse = false;
    public static bool OvenInUse = false;

    private bool Dead = false;
    private bool OnCupboard = false;

    //Shrink Speed and Scale
    public float TargetSpeed = 0.001f;
    public float ShrinkSpeed = 0.1f;

    public GameObject Cupboard;
    public GameObject Knife;
    public GameObject Name;
    public GameObject CutIngredient;
    public GameObject Burner;


    Collider ObjectCollider;
    private Rigidbody ThisRigidBody = null;
    // Start is called before the first frame update
    void Start()
    {
        Cupboard = GameObject.Find("Cupboard_Double_001/CuttingSpot");
        Knife = GameObject.Find("Knife_001");
        CutIngredient = GameObject.Find("CutTomato_001");
        Name = this.gameObject;
        ThisRigidBody = GetComponent<Rigidbody>();
        ObjectCollider = GetComponent<Collider>();
        Burner = GameObject.Find("Oven_001/Burner");
        PickupTest PickupScript = this.gameObject.GetComponent<PickupTest>();
        OvenUtensilTest OvenUtensilScript = this.gameObject.GetComponent<OvenUtensilTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Dead == true)
        {
            //reduce in size and shrink as it descends into the pot
            Name.transform.localScale = new Vector3(0.15f, 0.7f, 0.15f);
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
        if((collision.gameObject.tag == "Oven") && (OvenUtensilTest.OvenInUse == true))
        {
            Dead = true;
            Drop();
            Debug.Log("Attention");
        }

        if((collision.gameObject.tag == "Cupboard") && (gameObject.GetComponent<PickupTest>().ThisItemIsBeingCarried == true))
        {
            CupboardInUse = true;
            Drop();
        }
    }

    private void Drop()
    {
        if(OnCupboard == true)
        {
            //ThisRigidBody.isKinematic = true;
            Name.transform.position = Cupboard.transform.position;
            Name.transform.rotation = Cupboard.transform.rotation;
            Name.transform.SetParent(Cupboard.transform);
        }
        
        if(Dead == true)
        {
            ThisRigidBody.isKinematic = false;
            Debug.Log("Dead == " + Dead);
            Name.transform.position = Burner.transform.position;
            Name.transform.rotation = Burner.transform.rotation;
            Name.transform.SetParent(Burner.transform);
            Name.transform.localPosition = new Vector3(0f, 4f, 0f);
            ThisRigidBody.velocity = new Vector3(0f, -1f, 0f);
            //Destroy(this.gameObject, 2.5f);
        }
    }
}